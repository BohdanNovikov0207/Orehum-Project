// SPDX-FileCopyrightText: 2021 E F R <602406+Efruit@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2021 zlodo <zlodo@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Content.Shared.Localizations;

public static class Units
{
    public static readonly TypeTable Generic = new(
        // Table layout. Fite me.
        new TypeTable.Entry((null, 1e-24), 1e24, "si--y"),
        new TypeTable.Entry((1e-24, 1e-21), 1e21, "si--z"),
        new TypeTable.Entry((1e-21, 1e-18), 1e18, "si--a"),
        new TypeTable.Entry((1e-18, 1e-15), 1e15, "si--f"),
        new TypeTable.Entry((1e-15, 1e-12), 1e12, "si--p"),
        new TypeTable.Entry((1e-12, 1e-9), 1e9, "si--n"),
        new TypeTable.Entry((1e-9, 1e-3), 1e6, "si--u"),
        new TypeTable.Entry((1e-3, 1), 1e3, "si--m"),
        new TypeTable.Entry((1, 1000), 1, "si"),
        new TypeTable.Entry((1000, 1e6), 1e-4, "si-k"),
        new TypeTable.Entry((1e6, 1e9), 1e-6, "si-m"),
        new TypeTable.Entry((1e9, 1e12), 1e-9, "si-g"),
        new TypeTable.Entry((1e12, 1e15), 1e-12, "si-t"),
        new TypeTable.Entry((1e15, 1e18), 1e-15, "si-p"),
        new TypeTable.Entry((1e18, 1e21), 1e-18, "si-e"),
        new TypeTable.Entry((1e21, 1e24), 1e-21, "si-z"),
        new TypeTable.Entry((1e24, null), 1e-24, "si-y")
    );

    // N.B. We use kPa internally, so this is shifted one order of magnitude down.
    public static readonly TypeTable Pressure = new(
        new TypeTable.Entry((null, 1e-6), 1e9, "u--pascal"),
        new TypeTable.Entry((1e-6, 1e-3), 1e6, "m--pascal"),
        new TypeTable.Entry((1e-3, 1), 1e3, "pascal"),
        new TypeTable.Entry((1, 1000), 1, "k-pascal"),
        new TypeTable.Entry((1000, 1e6), 1e-4, "m-pascal"),
        new TypeTable.Entry((1e6, null), 1e-6, "g-pascal")
    );

    public static readonly TypeTable Power = new(
        new TypeTable.Entry((null, 1e-3), 1e6, "u--watt"),
        new TypeTable.Entry((1e-3, 1), 1e3, "m--watt"),
        new TypeTable.Entry((1, 1000), 1, "watt"),
        new TypeTable.Entry((1000, 1e6), 1e-4, "k-watt"),
        new TypeTable.Entry((1e6, 1e9), 1e-6, "m-watt"),
        new TypeTable.Entry((1e9, null), 1e-9, "g-watt")
    );

    public static readonly TypeTable Energy = new(
        new TypeTable.Entry((null, 1e-3), 1e6, "u--joule"),
        new TypeTable.Entry((1e-3, 1), 1e3, "m--joule"),
        new TypeTable.Entry((1, 1000), 1, "joule"),
        new TypeTable.Entry((1000, 1e6), 1e-4, "k-joule"),
        new TypeTable.Entry((1e6, 1e9), 1e-6, "m-joule"),
        new TypeTable.Entry((1e9, null), 1e-9, "g-joule")
    );

    public static readonly TypeTable Temperature = new(
        new TypeTable.Entry((null, 1e-3), 1e6, "u--kelvin"),
        new TypeTable.Entry((1e-3, 1), 1e3, "m--kelvin"),
        new TypeTable.Entry((1, 1e3), 1, "kelvin"),
        new TypeTable.Entry((1e3, 1e6), 1e-3, "k-kelvin"),
        new TypeTable.Entry((1e6, 1e9), 1e-6, "m-kelvin"),
        new TypeTable.Entry((1e9, null), 1e-9, "g-kelvin")
    );

    public static readonly Dictionary<string, TypeTable> Types = new()
    {
        ["generic"] = Generic,
        ["pressure"] = Pressure,
        ["power"] = Power,
        ["energy"] = Energy,
        ["temperature"] = Temperature,
    };

    public sealed class TypeTable
    {
        public readonly Entry[] E;

        public TypeTable(params Entry[] e)
        {
            E = e;
        }

        public bool TryGetUnit(double val, [NotNullWhen(true)] out Entry? winner)
        {
            Entry? w = default!;
            foreach (var e in E)
            {
                if ((e.Range.Min == null || e.Range.Min <= val) && (e.Range.Max == null || val < e.Range.Max))
                    w = e;
            }

            winner = w;
            return w != null;
        }

        public string Format(double val)
        {
            if (TryGetUnit(val, out var w))
                return val * w.Factor + " " + Loc.GetString("units-" + w.Unit);

            return val.ToString(CultureInfo.InvariantCulture);
        }

        public string Format(double val, string fmt)
        {
            if (TryGetUnit(val, out var w))
                return (val * w.Factor).ToString(fmt) + " " + Loc.GetString("units-" + w.Unit);

            return val.ToString(fmt);
        }

        public sealed class Entry
        {
            // Factor is a number that the value will be multiplied by
            // to adjust it in to the proper range.
            public readonly double Factor;

            // Any item within [Min, Max) is considered to be in-range
            // of this Entry.
            public readonly (double? Min, double? Max) Range;

            // Unit is an ID for Fluent. All Units are prefixed with
            // "units-" internally. Usually follows the format $"{unit-abbrev}-{prefix}".
            //
            // Example: "si-g" is actually processed as "units-si-g"
            //
            // As a matter of style, units for values less than 1 (i.e. mW)
            // should have two dashes before their prefix.
            public readonly string Unit;

            public Entry((double?, double?) range, double factor, string unit)
            {
                Range = range;
                Factor = factor;
                Unit = unit;
            }
        }
    }
}
