// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 moonheart08 <moonheart08@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client.Guidebook.Richtext;

/// <summary>
/// A document, containing arbitrary text and UI elements.
/// </summary>
public sealed class Document : BoxContainer, IDocumentTag
{
    public Document()
    {
        Orientation = LayoutOrientation.Vertical;
    }

    public bool TryParseTag(Dictionary<string, string> args, [NotNullWhen(true)] out Control? control)
    {
        DebugTools.Assert(args.Count == 0);
        control = this;
        return true;
    }
}

public interface IDocumentTag
{
    bool TryParseTag(Dictionary<string, string> args, [NotNullWhen(true)] out Control? control);
}
