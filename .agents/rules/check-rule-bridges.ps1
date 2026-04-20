param(
    [string]$RepoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\\..")).Path
)

$ErrorActionPreference = "Stop"

$sourceRoot = Join-Path $RepoRoot ".agent/rules"
$codexBridgeRoot = Join-Path $RepoRoot ".agents/rules"

function Get-RuleData {
    param([string]$RulePath)

    $raw = Get-Content $RulePath -Raw
    $fmMatch = [regex]::Match($raw, "(?ms)^---\r?\n(.*?)\r?\n---")
    if (-not $fmMatch.Success) {
        throw "No YAML frontmatter in $RulePath"
    }

    $frontmatter = $fmMatch.Groups[1].Value
    $body = $raw.Substring($fmMatch.Index + $fmMatch.Length).Trim()
    $trigger = [regex]::Match($frontmatter, "(?m)^trigger:\s*(.+)$").Groups[1].Value.Trim()
    $sourceRule = [regex]::Match(
        $frontmatter,
        "(?m)^\s*source_rule:\s*`"?([^`"\r\n]+)`"?\s*$"
    ).Groups[1].Value.Trim()

    return @{
        raw = $raw
        body = $body
        trigger = $trigger
        source_rule = $sourceRule
    }
}

if (-not (Test-Path $sourceRoot)) {
    throw "Source rules path not found: $sourceRoot"
}
if (-not (Test-Path $codexBridgeRoot)) {
    throw "Codex bridge rules path not found: $codexBridgeRoot"
}


$errors = New-Object System.Collections.Generic.List[string]

$sourceRules = Get-ChildItem $sourceRoot -File -Filter "*.md" |
    Where-Object { $_.Name -ne "README.md" } |
    Sort-Object Name
$codexBridgeRules = Get-ChildItem $codexBridgeRoot -File -Filter "*.md" |
    Where-Object { $_.Name -ne "AUTHORING_POLICY.md" } |
    Sort-Object Name

$sourceNames = @($sourceRules.Name)
$codexBridgeNames = @($codexBridgeRules.Name)

foreach ($source in $sourceRules) {
    $name = $source.Name
    $sourceRuleMd = $source.FullName
    $codexBridgeRuleMd = Join-Path $codexBridgeRoot $name

    if (-not (Test-Path $codexBridgeRuleMd)) {
        $errors.Add("Missing Codex bridge rule for '$name'.")
    }

    $sourceData = Get-RuleData -RulePath $sourceRuleMd
    $codexData = Get-RuleData -RulePath $codexBridgeRuleMd

    $expectedSourceRule = "../../../.agent/rules/$name"
    $expectedCodexBridgeRef = "../../../.agents/rules/$name"

    if ($codexData.trigger -ne $sourceData.trigger) {
        $errors.Add("Codex bridge trigger mismatch for '$name'.")
    }
    if ($codexData.source_rule -ne $expectedSourceRule) {
        $errors.Add(
            "Codex bridge source_rule mismatch for '$name': '$($codexData.source_rule)'"
        )
    }
    if ($codexData.body -notmatch [regex]::Escape($expectedSourceRule)) {
        $errors.Add("Codex bridge reference mismatch for '$name'.")
    }

}

foreach ($codexBridgeName in $codexBridgeNames) {
    if ($sourceNames -notcontains $codexBridgeName) {
        $errors.Add("Codex bridge rule exists without source rule: '$codexBridgeName'.")
    }
}

foreach ($sourceName in $sourceNames) {
    if ($codexBridgeNames -notcontains $sourceName) {
        $errors.Add("Source rule missing in Codex bridge tree: '$sourceName'.")
    }
}

if ($errors.Count -gt 0) {
    Write-Host "Rule bridge check failed with $($errors.Count) issue(s):" -ForegroundColor Red
    foreach ($errorItem in $errors) {
        Write-Host "- $errorItem"
    }
    exit 1
}

Write-Host "Rule bridge check passed:"
Write-Host "- source rules: $($sourceRules.Count)"
Write-Host "- codex bridges: $($codexBridgeRules.Count)"
