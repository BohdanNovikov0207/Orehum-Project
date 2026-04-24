// SPDX-FileCopyrightText: 2024 Menshin <Menshin@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.Atmos.Piping.Portable.Components;

[Serializable]
[NetSerializable]
public enum SpaceHeaterUiKey
{
    Key,
}

[Serializable]
[NetSerializable]
public sealed class SpaceHeaterToggleMessage : BoundUserInterfaceMessage
{
}

[Serializable]
[NetSerializable]
public sealed class SpaceHeaterChangeTemperatureMessage : BoundUserInterfaceMessage
{
    public SpaceHeaterChangeTemperatureMessage(float temperature)
    {
        Temperature = temperature;
    }

    public float Temperature { get; }
}

[Serializable]
[NetSerializable]
public sealed class SpaceHeaterChangePowerLevelMessage : BoundUserInterfaceMessage
{
    public SpaceHeaterChangePowerLevelMessage(SpaceHeaterPowerLevel powerLevel)
    {
        PowerLevel = powerLevel;
    }

    public SpaceHeaterPowerLevel PowerLevel { get; }
}

[Serializable]
[NetSerializable]
public sealed class SpaceHeaterChangeModeMessage : BoundUserInterfaceMessage
{
    public SpaceHeaterChangeModeMessage(SpaceHeaterMode mode)
    {
        Mode = mode;
    }

    public SpaceHeaterMode Mode { get; }
}

[Serializable]
[NetSerializable]
public sealed class SpaceHeaterBoundUserInterfaceState : BoundUserInterfaceState
{
    public SpaceHeaterBoundUserInterfaceState(float minTemperature,
        float maxTemperature,
        float temperature,
        bool enabled,
        SpaceHeaterMode mode,
        SpaceHeaterPowerLevel powerLevel)
    {
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
        TargetTemperature = temperature;
        Enabled = enabled;
        Mode = mode;
        PowerLevel = powerLevel;
    }

    public float MinTemperature { get; }
    public float MaxTemperature { get; }
    public float TargetTemperature { get; }
    public bool Enabled { get; }
    public SpaceHeaterMode Mode { get; }
    public SpaceHeaterPowerLevel PowerLevel { get; }
}

[Serializable] [NetSerializable]
public enum SpaceHeaterMode : byte
{
    Auto,
    Heat,
    Cool,
}

[Serializable] [NetSerializable]
public enum SpaceHeaterPowerLevel : byte
{
    Low,
    Medium,
    High,
}
