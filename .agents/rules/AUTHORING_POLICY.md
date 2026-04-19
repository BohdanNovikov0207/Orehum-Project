# Rule Authoring Policy

## Scope

This repository uses five rule trees:

- `.agent/rules` is the source of truth.
- `.agents/rules` is the Codex compatibility layer.

## Required Rule

For every rule file in `.agent/rules/<rule-name>.md`, keep matching bridge files:

- `.agents/rules/<rule-name>.md`

When creating, updating, renaming, or deleting a rule in `.agent/rules`, apply the same
change in all bridge trees in the same pull request.

## Bridge Contract

Each Codex bridge rule file must contain:

- `trigger`: synchronized copy of canonical trigger from `.agent/rules/<rule-name>.md`.
- `metadata.source_rule`: `../../../.agent/rules/<rule-name>.md`.

## PR Checklist Gate

A PR is incomplete if any rule exists in `.agent/rules` without matching bridges in
`.agents/rules`.

Run this check before pushing:

`pwsh ./.agents/rules/check-rule-bridges.ps1`
