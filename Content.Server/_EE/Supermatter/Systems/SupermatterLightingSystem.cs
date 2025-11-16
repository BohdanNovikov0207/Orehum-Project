using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Content.Server.Lightning;
using Content.Shared._EE.CCVars;
using Content.Shared._EE.Supermatter.Components;
using Robust.Shared.Configuration;
using Robust.Shared.Random;

namespace Content.Server._EE.Supermatter.Systems;

public sealed partial class SupermatterSystem

{
    private TimeSpan _zapAccumulator = TimeSpan.Zero;

    /// <summary>
    /// Shoot lightning bolts depending on accumulated power, but only once per interval.
    /// </summary>
    public void SupermatterZap(EntityUid uid, SupermatterComponent sm, float frameTime)
    {
        if (!sm.HasBeenPowered)
            return;

        _zapAccumulator += TimeSpan.FromSeconds(frameTime);

        var zapTimer = sm.ZapTimer;

        while (_zapAccumulator >= zapTimer)
        {
            _zapAccumulator -= zapTimer;

            var power = sm.Power;
            var integrity = GetIntegrity(sm);
            var zapRange = Math.Clamp(power / 1000, 3, 7);
            int zapCount = 1;

            if (_random.Prob(0.2f))
                zapCount++;

            if (power >= _config.GetCVar(CCVars.SupermatterPowerMinPenaltyThreshold))
            {
                var powerZapCount = Math.Clamp(power / 3000, 1, 2);
                zapCount = (int)(zapCount + powerZapCount);
            }

            if (integrity < 50)
            {
                zapCount++;
                zapTimer /= 2;
            }

            zapCount = Math.Clamp(zapCount, 1, 5);

            int zapPower = 0;

            if (power >= _config.GetCVar(CCVars.SupermatterSeverePowerPenaltyThreshold))
            {
                zapPower++;
                zapTimer -= TimeSpan.FromSeconds(10);
            }

            if (power >= _config.GetCVar(CCVars.SupermatterCriticalPowerPenaltyThreshold))
            {
                zapPower++;
                zapTimer -= TimeSpan.FromSeconds(5);
            }

            zapPower = Math.Clamp(zapPower, 1, 3);

            _lightning.ShootRandomLightnings(uid, zapRange, zapCount, sm.LightningPrototypes[zapPower]);
        }

    }
}
