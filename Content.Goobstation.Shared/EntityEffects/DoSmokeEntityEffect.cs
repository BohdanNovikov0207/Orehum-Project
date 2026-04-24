// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 TheBorzoiMustConsume <197824988+TheBorzoiMustConsume@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Chemistry.Components;
using Content.Shared.Database;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.EntityEffects;

/// <summary>
/// Creates smoke similar to SmokeOnTrigger
/// </summary>
public sealed partial class DoSmokeEntityEffect : EventEntityEffect<DoSmokeEntityEffect>
{
    /// <summary>
    /// How long the smoke stays for, after it has spread.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public float Duration = 10;

    /// <summary>
    /// Smoke entity to spawn.
    /// Defaults to smoke but you can use foam if you want.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public EntProtoId SmokePrototype = "Smoke";

    /// <summary>
    /// Solution to add to each smoke cloud.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public Solution Solution = new();

    /// <summary>
    /// How much the smoke will spread.
    /// </summary>
    [DataField(required: true)] [ViewVariables(VVAccess.ReadWrite)]
    public int SpreadAmount;

    public DoSmokeEntityEffect(float duration, int spreadAmount, EntProtoId smokePrototype, Solution solution)
    {
        Duration = duration;
        SpreadAmount = spreadAmount;
        SmokePrototype = smokePrototype;
        Solution = solution;
    }

    public override LogImpact LogImpact => LogImpact.Medium;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;

    public override void Effect(EntityEffectBaseArgs args)
    {
        if (args is not EntityEffectReagentArgs reagentArgs)
            return;

        var ev = new DoSmokeEntityEffect(Duration, SpreadAmount, SmokePrototype, Solution);
        args.EntityManager.EventBus.RaiseLocalEvent(args.TargetEntity, ev);
    }
}
