// SPDX-FileCopyrightText: 2022 Flipp Syder <76629141+vulppine@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.DeviceNetwork;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
// Goobstation

namespace Content.Shared.SurveillanceCamera;

// Camera monitor state. If the camera is null, there should be a blank
// space where the camera is.
[Serializable] [NetSerializable]
public sealed class SurveillanceCameraMonitorUiState : BoundUserInterfaceState
{
    // Currently available subnets. Does not send the entirety of the possible
    // cameras to view because that could be really, really large

    public string ActiveAddress;

    public SurveillanceCameraMonitorUiState(NetEntity? activeCamera,
        string activeAddress,
        Dictionary<string, (string, (NetEntity, NetCoordinates))> cameras,
        Dictionary<string, (string, (NetEntity, NetCoordinates))> mobileCameras) // Goobstation
    {
        ActiveCamera = activeCamera;
        ActiveAddress = activeAddress;
        Cameras = cameras;
        MobileCameras = mobileCameras; // Goobstation
    }

    // The active camera on the monitor. If this is null, the part of the UI
    // that contains the monitor should clear.
    public NetEntity? ActiveCamera { get; }

    // Known cameras, by address and name.
    public Dictionary<string, (string, (NetEntity, NetCoordinates))> Cameras { get; } // Goobstation

    public Dictionary<string, (string, (NetEntity, NetCoordinates))> MobileCameras { get; } // Goobstation
}

[Serializable] [NetSerializable]
public sealed class SurveillanceCameraMonitorSwitchMessage : BoundUserInterfaceMessage
{
    public SurveillanceCameraMonitorSwitchMessage(string address)
    {
        Address = address;
    }

    public string Address { get; }
}

[Serializable] [NetSerializable]
public sealed class SurveillanceCameraMonitorSubnetRequestMessage : BoundUserInterfaceMessage
{
    public SurveillanceCameraMonitorSubnetRequestMessage(string subnet)
    {
        Subnet = subnet;
    }

    public string Subnet { get; }
}

// Sent when the user requests that the cameras on the current subnet be refreshed.
[Serializable] [NetSerializable]
public sealed class SurveillanceCameraRefreshCamerasMessage : BoundUserInterfaceMessage
{
}

// Sent when the user requests that the subnets known by the monitor be refreshed.
[Serializable] [NetSerializable]
public sealed class SurveillanceCameraRefreshSubnetsMessage : BoundUserInterfaceMessage
{
}

// Sent when the user wants to disconnect the monitor from the camera.
[Serializable] [NetSerializable]
public sealed class SurveillanceCameraDisconnectMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public enum SurveillanceCameraMonitorUiKey : byte
{
    Key,
}

// SETUP

[Serializable] [NetSerializable]
public sealed class SurveillanceCameraSetupBoundUiState : BoundUserInterfaceState
{
    public SurveillanceCameraSetupBoundUiState(string name,
        uint network,
        List<ProtoId<DeviceFrequencyPrototype>> networks,
        bool nameDisabled,
        bool networkDisabled)
    {
        Name = name;
        Network = network;
        Networks = networks;
        NameDisabled = nameDisabled;
        NetworkDisabled = networkDisabled;
    }

    public string Name { get; }
    public uint Network { get; }
    public List<ProtoId<DeviceFrequencyPrototype>> Networks { get; }
    public bool NameDisabled { get; }
    public bool NetworkDisabled { get; }
}

[Serializable] [NetSerializable]
public sealed class SurveillanceCameraSetupSetName : BoundUserInterfaceMessage
{
    public SurveillanceCameraSetupSetName(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[Serializable] [NetSerializable]
public sealed class SurveillanceCameraSetupSetNetwork : BoundUserInterfaceMessage
{
    public SurveillanceCameraSetupSetNetwork(int network)
    {
        Network = network;
    }

    public int Network { get; }
}

[Serializable] [NetSerializable]
public enum SurveillanceCameraSetupUiKey : byte
{
    Camera,
    Router,
}
