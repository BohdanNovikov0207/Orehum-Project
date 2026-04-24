// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Marcus F <199992874+thebiggestbruh@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Atmos;

namespace Content.Goobstation.Shared.Temperature;

public sealed class TemperatureImmunityEvent(float currentTemperature) : EntityEventArgs
{
    public readonly float IdealTemperature = Atmospherics.T37C;
    public float CurrentTemperature = currentTemperature;
}

[ByRefEvent]
public record struct BeforeTemperatureChange(
    float CurrentTemperature,
    float LastTemperature,
    float TemperatureDelta);
