// SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 fishfish458 <fishfish458>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Server.Cloning.Components;

[RegisterComponent]
public sealed partial class CloningConsoleComponent : Component
{
    public const string ScannerPort = "MedicalScannerSender";

    public const string PodPort = "CloningPodSender";

    [ViewVariables]
    public EntityUid? CloningPod = null;

    public bool CloningPodInRange = true;

    [ViewVariables]
    public EntityUid? GeneticScanner = null;

    public bool GeneticScannerInRange = true;

    /// Maximum distance between console and one if its machines
    [DataField("maxDistance")]
    public float MaxDistance = 4f;
}
