using Robust.Shared.Serialization;

namespace Content.Shared.Power;

/// <summary>
/// UI key for large battery (SMES/substation) UIs.
/// </summary>
[NetSerializable] [Serializable]
public enum BatteryUiKey : byte
{
    Key,
}

/// <summary>
/// UI state for large battery (SMES/substation) UIs.
/// </summary>
/// <seealso cref="BatteryUiKey" />
[Serializable] [NetSerializable]
public sealed class BatteryBuiState : BoundUserInterfaceState
{
    // These are mostly just regular Pow3r parameters.

    // I/O
    public bool CanCharge;
    public bool CanDischarge;
    public float Capacity;

    // Storage
    public float Charge;
    public float CurrentReceiving;
    public float CurrentSupply;
    public float Efficiency;
    public bool LoadingNetworkHasPower;

    // Charge
    public float MaxChargeRate;
    public float MaxMaxChargeRate;
    public float MaxMaxSupply;

    // Discharge
    public float MaxSupply;
    public float MinMaxChargeRate;
    public float MinMaxSupply;
    public bool SupplyingNetworkHasPower;
}

/// <summary>
/// Sent client to server to change the input breaker state on a large battery.
/// </summary>
[Serializable] [NetSerializable]
public sealed class BatterySetInputBreakerMessage(bool on) : BoundUserInterfaceMessage
{
    public bool On = on;
}

/// <summary>
/// Sent client to server to change the output breaker state on a large battery.
/// </summary>
[Serializable] [NetSerializable]
public sealed class BatterySetOutputBreakerMessage(bool on) : BoundUserInterfaceMessage
{
    public bool On = on;
}

/// <summary>
/// Sent client to server to change the charge rate on a large battery.
/// </summary>
[Serializable] [NetSerializable]
public sealed class BatterySetChargeRateMessage(float rate) : BoundUserInterfaceMessage
{
    public float Rate = rate;
}

/// <summary>
/// Sent client to server to change the discharge rate on a large battery.
/// </summary>
[Serializable] [NetSerializable]
public sealed class BatterySetDischargeRateMessage(float rate) : BoundUserInterfaceMessage
{
    public float Rate = rate;
}
