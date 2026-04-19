---
trigger: always_on
---

# Rule: Defining the codebase prefix, project folder and edit markers

This rule is mandatory for any task in the SS14 forks of the Orehum family.

## 1. What you need to determine before starting work

Before analyzing, planning paths and making changes, always fix three values:

1. Active codebase prefix (`Orehum`).
2. Forked project folder (`_Orehum`).
3. The text of the edit marker that should be used in comments.

Don't start editing vanilla files until these three values ​​are defined.

## 2. How to determine the current fork

Define a fork in this order:

1. First look at git remote slug (`origin`, then `upstream`, then the rest remote).
2. Then look at the name of the repository root folder and the path of the working directory.
3. Then look at which forked project folder is actually present in the code base (`_Orehum_`).
4. Then look at the nearest existing edit markers in the adjacent code.

If the signals diverge:

1. Priority goes to git remote and the actual project folder.
2. If remote points to an alias/mirror, but folder and existing tokens are unambiguous, use an actual local fork.
3. Do not mix markers from different forks within the same task.

## 3. Correspondence map

Select the first line that matches `repo slug`, the name of the root folder, alias, or an actual fork folder:

| Match | Prefix | Project folder | Single-line marker | Block markers | Note |
| --- | --- | --- | --- | --- | --- |
| `BohdanNovikov0207/Orehum-Project`, `_Orehum_` | `Orehum` | `_Orehum` | `Orehum-Edit` | `Orehum edit start/end`, `Orehum added start/end` | For new single placemarks, default to `Orehum-Edit`. |


## 4. How to apply a marker in a specific file

The same general rules apply for any fork:

1. Use `Prefix`, `Project folder` and `marker` from the selected table row.
2. Do not change the marker text, just adapt the comment syntax to the file language.
3. If the file already uses a local style of the same fork, continue with it. Don't repurpose old markers just for cosmetics.

Select the comment syntax to match the file language:

- C#, C++, Java: `// Orehum-Edit`, `// Orehum added start - reason`
- YAML, FTL, Python, Shell: `# Orehum-Edit`, `# Orehum added start - reason`
- XML, HTML: `<!-- Orehum-Edit -->`, only if comments in this format are allowed and really needed

## 5. How does this affect the structure of edits?

1. Place new forked files in the corresponding project folder of the current fork.
2. Mark minimal hooks in vanilla files with the edit marker of the current fork.
4. If a project is already mixing historical marker variants within one fork, for new edits follow the closest local style of that fork, but do not switch to markers from another fork.
