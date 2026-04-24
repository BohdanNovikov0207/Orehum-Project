// SPDX-FileCopyrightText: 2021 20kdc <asdd2808@gmail.com>
// SPDX-FileCopyrightText: 2021 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.NodeContainer;

public static class NodeVis
{
    [Serializable] [NetSerializable]
    public sealed class MsgEnable : EntityEventArgs
    {
        public MsgEnable(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }
    }

    [Serializable] [NetSerializable]
    public sealed class MsgData : EntityEventArgs
    {
        public Dictionary<int, string?> GroupDataUpdates = new();
        public List<int> GroupDeletions = new();
        public List<GroupData> Groups = new();
    }

    [Serializable] [NetSerializable]
    public sealed class GroupData
    {
        public Color Color;
        public string? DebugData;
        public string GroupId = "";
        public int NetId;
        public NodeDatum[] Nodes = Array.Empty<NodeDatum>();
    }

    [Serializable] [NetSerializable]
    public sealed class NodeDatum
    {
        public NetEntity Entity;
        public string Name = "";
        public int NetId;
        public int[] Reachable = Array.Empty<int>();
        public string Type = "";
    }
}
