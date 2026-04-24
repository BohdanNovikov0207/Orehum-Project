// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Collections;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Dataset;

/// <summary>
/// A variant of <see cref="DatasetPrototype" /> intended to specify a sequence of LocId strings
/// without having to copy-paste a ton of LocId strings into the YAML.
/// </summary>
[Prototype]
public sealed class LocalizedDatasetPrototype : IPrototype
{
    /// <summary>
    /// Collection of LocId strings.
    /// </summary>
    [DataField]
    public LocalizedDatasetValues Values { get; private set; } = [];

    /// <summary>
    /// Identifier for this prototype.
    /// </summary>
    [ViewVariables]
    [IdDataField]
    public string ID { get; } = default!;
}

[Serializable] [NetSerializable]
[DataDefinition]
public sealed partial class LocalizedDatasetValues : IReadOnlyList<string>
{
    /// <summary>
    /// String prepended to the index number to generate each LocId string.
    /// For example, a prefix of <c>tips-dataset-</c> will generate <c>tips-dataset-1</c>,
    /// <c>tips-dataset-2</c>, etc.
    /// </summary>
    [DataField(required: true)]
    public string Prefix { get; private set; } = default!;

    /// <summary>
    /// How many values are in the dataset.
    /// </summary>
    [DataField(required: true)]
    public int Count { get; private set; }

    public string this[int index]
    {
        get
        {
            if (index >= Count || index < 0)
                throw new IndexOutOfRangeException();
            return Prefix + (index + 1);
        }
    }

    public IEnumerator<string> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public sealed class Enumerator : IEnumerator<string>
    {
        private readonly LocalizedDatasetValues _values;
        private int _index; // Whee, 1-indexing

        public Enumerator(LocalizedDatasetValues values)
        {
            _values = values;
        }

        public string Current => _values.Prefix + _index;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _index++;
            return _index <= _values.Count;
        }

        public void Reset() => _index = 0;
    }
}
