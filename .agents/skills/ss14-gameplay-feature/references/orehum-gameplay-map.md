# Open-Space Gameplay Map

## Purpose

Use this file to route a gameplay task to the correct assemblies, feature folders, paired resource files, and validation steps before editing.

## Fast Triage

1. Name the player-facing mechanic or admin behavior.
2. Match it to the closest top-level domain folder. Most SS14 areas reuse the same folder name across `Content.Shared`, `Content.Server`, `Content.Client`, and `Resources`.
3. Start from `Content.Shared/<Domain>/` if the feature crosses networking, prediction, UI state, or both client and server.
4. Check `Resources/Prototypes/`, `Resources/Locale/en-US/`, `Resources/Textures/`, and `Resources/Audio/` immediately for the same domain.
5. Check `Content.Tests/` or `Content.IntegrationTests/` for the same mechanic when behavior changes are risky.

## Assembly Ownership

- `Content.Shared/`: data components, shared events, predicted logic, networked state, shared BUI state, and shared helper types.
- `Content.Server/`: authoritative simulation, round rules, admin commands, persistence, server-only side effects, and map/entity spawning rules.
- `Content.Client/`: visuals, overlays, XAML, BUIs, local-only polish, prediction presentation, and input affordances.
- `Resources/`: prototypes, localization, sprites, sounds, maps, and other content data.

## Common Cross-Assembly Shapes

- Shared component plus server system plus client visualizer.
- Shared action or event plus server validation plus client popup or audio.
- Shared BUI state and messages plus server handler plus client window.
- Server-only rule system backed by prototypes and localized text.
- Shared helper or component plus client-only UI and server-only persistence.

## Common Domain Clusters

### Character, damage, and interaction

- `Body`, `Mobs`, `Humanoid`, `Hands`, `Inventory`, `Clothing`, `Damage`, `Medical`, `Species`, `Metabolism`, `Cuffs`, `Stunnable`, `Movement`
- Reach here for body parts, health, equipment, restraints, movement modifiers, and status changes.

### Item and action flow

- `Actions`, `Interaction`, `Item`, `Storage`, `Tools`, `Throwing`, `Prying`, `Resist`, `Wieldable`, `Placeable`
- Reach here for verbs, held items, interaction gating, tool use, throwing, storage, and equipment actions.

### Station infrastructure

- `Atmos`, `Power`, `SMES`, `APC`, `Machines`, `DeviceNetwork`, `DeviceLinking`, `NodeContainer`, `Construction`, `Wires`, `Shuttles`, `Station`
- Reach here for machines, wiring, atmospherics, power distribution, construction graphs, and station-level services.

### Roundflow and administration

- `Objectives`, `Roles`, `Antag`, `GameTicking`, `NukeOps`, `Revolutionary`, `Thief`, `Traitor`, `Administration`, `Preferences`, `Players`, `Respawn`
- Reach here for role assignment, antagonist logic, round lifecycle, player sessions, and admin-facing behavior.

### Presentation and feedback

- `Audio`, `Effects`, `Popups`, `Sprite`, `Alert`, `StatusEffect`, `Guidebook`, `Overlays`, `UserInterface`
- Reach here for client-visible feedback, overlays, alert state, guidebook content, and shared-to-client presentation hooks.

## Server-Only Hotspots

These folders currently exist only in `Content.Server/` and usually represent authority-only behavior or operational services:

- `Acz`, `Afk`, `Announcements`, `Chunking`, `Codewords`, `Connection`, `CPUJob`, `Database`, `Discord`, `ExCable`
- `ForceAttack`, `Gatherable`, `GuideGenerator`, `ImmovableRod`, `IP`, `Jobs`, `KillTracking`, `Motd`
- `PowerSink`, `RandomAppearance`, `RandomMetadata`, `RequiresGrid`, `Screens`, `ServerInfo`, `ServerUpdates`
- `Spawners`, `StationEvents`, `Tesla`, `Traitor`, `VentHorde`, `Vocalization`, `VoiceTrigger`

If a task lands in one of these areas, expect the logic to be server-authoritative with little or no mirrored client structure.

## Client-Only Hotspots

These folders currently exist only in `Content.Client/` and usually represent UI, visuals, debug tooling, or local presentation:

- `Alerts`, `Animations`, `Changelog`, `Clickable`, `CloningConsole`, `ContextMenu`, `Cooldown`, `Credits`
- `DamageState`, `DebugMon`, `FeedbackPopup`, `FlavorText`, `Fullscreen`, `Gameplay`, `Graphics`, `HealthAnalyzer`
- `Interactable`, `Items`, `Kudzu`, `LateJoin`, `Launcher`, `Lobby`, `MainMenu`, `Markers`, `Message`
- `NetworkConfigurator`, `Options`, `Orbit`, `Outline`, `Playtime`, `Replay`, `Resources`, `RichText`
- `Screenshot`, `Stylesheets`, `Viewport`

If a task only touches these areas, it is usually presentation work and should not introduce new shared or server dependencies without a clear need.

## Shared Utility Buckets

Some useful buckets are intentionally shared-first even when they do not have direct server/client peers for the same name:

- `ActionBlocker`, `APC`, `Blocking`, `Climbing`, `ComponentTable`, `DetailExaminable`, `Execution`, `Friction`
- `Glue`, `HealthExaminable`, `Internals`, `Metabolism`, `Prototypes`, `Repairable`, `Rotatable`, `Spawning`
- `StatusEffect`, `Timing`, `Warps`, `Whistle`

Treat these as infrastructure or shared gameplay primitives that other domains compose.

## Open-Space Fork-Specific Areas

The fork already has custom `_Orehum` content across code and resources. Always inspect these before assuming the upstream pattern is the full story.

- `Content.Shared/_Orehum/Movement/Pulling/Components/`: shared pulling-related data such as `ChokedComponent`.
- `Content.Server/_Orehum/Administration/Commands/`: server-only custom commands such as lobby-music control.
- `Content.Server/_Orehum/ChatFilter/`: custom chat filtering.
- `Content.Server/_Orehum/Effects/`: authoritative part of fork-specific effect handling.
- `Content.Server/_Orehum/Movement/Pulling/`: fork-specific suffocation or pulling consequences.
- `Content.Server/_Orehum/Throwing/`: server-side throw impact behavior.
- `Content.Client/_Orehum/Effects/`: client component and visual system for outline flash.
- `Resources/Prototypes/_Orehum/Entities/...`: fork-specific prototypes; current examples include custom marking trees.
- `Resources/Locale/ru-RU/_Orehum/...`: fork-specific FTL for commands and markings.

When adding new fork behavior, prefer extending these `_Orehum` areas instead of hiding fork logic inside upstream folders without a reason.

## Code-To-Data Pairing Rules

- If you add or rename a component field that is serialized, check prototypes using it.
- If you add a new popup, action, UI label, or admin-facing string, add FTL.
- If you add reusable audio, prefer a sound collection prototype instead of hardcoded file paths.
- If you add a reusable visual state, check whether it belongs in a shared appearance component, a client visualizer, or a sprite RSI.
- If you add a new prototype family, keep parents in `base.yml` and variants in neighboring files.

## Useful Next References

- `../feature-checklist.md`
- `../../ss14-client-server-shared/references/client-server-primer.md`
- `../../ss14-client-server-shared/references/shared-and-prediction.md`
- `../../ss14-prototype-basics/references/first-prototype-workflow.md`
