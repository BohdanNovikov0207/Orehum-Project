# Edit Types

## Preferred Edit Order

1. configuration or prototype-only edit
2. extend an existing public system API
3. narrow patch in an upstream content file
4. fork-only sidecar file under `_Orehum`
5. engine edit as explicit last-resort escalation

## Rule

Choose the earliest option that fully solves the task without hiding fork behavior in unrelated files, duplicating logic, or hardcoding a one-off case that should stay reusable.

When option 3 requires Orehum-specific code in a file outside `_Orehum`, keep the patch narrow and wrap each Orehum-specific block:

```csharp
// Orehum edit start
...code here...
// Orehum edit end
```

Use the file's native comment syntax for non-C# files while preserving the marker text.
