param(
    [string]$RepoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\\..")).Path
)

$ErrorActionPreference = "Stop"

$sourceRoot = Join-Path $RepoRoot ".agent/skills"
$codexBridgeRoot = Join-Path $RepoRoot ".agents/skills"

function Get-SkillData {
    param([string]$SkillMdPath)

    $raw = Get-Content $SkillMdPath -Raw
    $fmMatch = [regex]::Match($raw, "(?ms)^---\r?\n(.*?)\r?\n---")
    if (-not $fmMatch.Success) {
        throw "No YAML frontmatter in $SkillMdPath"
    }

    $frontmatter = $fmMatch.Groups[1].Value
    $body = $raw.Substring($fmMatch.Index + $fmMatch.Length).Trim()
    $name = [regex]::Match($frontmatter, "(?m)^name:\s*(.+)$").Groups[1].Value.Trim()
    $description = [regex]::Match($frontmatter, "(?m)^description:\s*(.+)$").Groups[1].Value.Trim()
    $sourceSkill = [regex]::Match(
        $frontmatter,
        "(?m)^\s*source_skill:\s*`"?([^`"\r\n]+)`"?\s*$"
    ).Groups[1].Value.Trim()

    return @{
        raw = $raw
        body = $body
        name = $name
        description = $description
        source_skill = $sourceSkill
    }
}

if (-not (Test-Path $sourceRoot)) {
    throw "Source skills path not found: $sourceRoot"
}
if (-not (Test-Path $codexBridgeRoot)) {
    throw "Codex bridge skills path not found: $codexBridgeRoot"
}

$errors = New-Object System.Collections.Generic.List[string]

$sourceSkills = Get-ChildItem $sourceRoot -Directory |
    Where-Object { Test-Path (Join-Path $_.FullName "SKILL.md") } |
    Sort-Object Name
$codexBridgeSkills = Get-ChildItem $codexBridgeRoot -Directory |
    Where-Object { Test-Path (Join-Path $_.FullName "SKILL.md") } |
    Sort-Object Name

$sourceNames = @($sourceSkills.Name)
$codexBridgeNames = @($codexBridgeSkills.Name)

foreach ($source in $sourceSkills) {
    $name = $source.Name
    $sourceSkillMd = Join-Path $source.FullName "SKILL.md"
    $codexBridgeSkillMd = Join-Path $codexBridgeRoot "$name/SKILL.md"

    if (-not (Test-Path $codexBridgeSkillMd)) {
        $errors.Add("Missing Codex bridge SKILL.md for '$name'.")
    }

    $sourceData = Get-SkillData -SkillMdPath $sourceSkillMd
    $codexData = Get-SkillData -SkillMdPath $codexBridgeSkillMd

    $expectedSourceSkill = "../../../.agent/skills/$name/SKILL.md"
    $expectedCodexBridgeRef = "../../../.agents/skills/$name/SKILL.md"

    if ($codexData.name -ne $name) {
        $errors.Add("Codex bridge name mismatch for '$name': '$($codexData.name)'")
    }
    if ($codexData.description -ne $sourceData.description) {
        $errors.Add("Codex bridge description mismatch for '$name'.")
    }
    if ($codexData.source_skill -ne $expectedSourceSkill) {
        $errors.Add(
            "Codex bridge source_skill mismatch for '$name': '$($codexData.source_skill)'"
        )
    }
}

foreach ($codexBridgeName in $codexBridgeNames) {
    if ($sourceNames -notcontains $codexBridgeName) {
        $errors.Add("Codex bridge exists without source skill: '$codexBridgeName'.")
    }
}

foreach ($sourceName in $sourceNames) {
    if ($codexBridgeNames -notcontains $sourceName) {
        $errors.Add("Source skill missing in Codex bridge tree: '$sourceName'.")
    }
}

if ($errors.Count -gt 0) {
    Write-Host "Bridge check failed with $($errors.Count) issue(s):" -ForegroundColor Red
    foreach ($errorItem in $errors) {
        Write-Host "- $errorItem"
    }
    exit 1
}

Write-Host "Bridge check passed:"
Write-Host "- source skills: $($sourceSkills.Count)"
Write-Host "- codex bridges: $($codexBridgeSkills.Count)"
exit 0
