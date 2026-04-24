// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Text.Json.Serialization;

namespace Content.PatreonParser;

public sealed class Data
{
    [JsonPropertyName("attributes")]
    public Attributes Attributes = default!;

    [JsonPropertyName("id")]
    public string Id = default!;

    [JsonPropertyName("relationships")]
    public Relationships Relationships = default!;

    [JsonPropertyName("type")]
    public string Type = default!;
}
