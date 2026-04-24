using System.Diagnostics.CodeAnalysis;
using Content.Shared.Preferences;
using Content.Shared.Preferences.Loadouts;
using Content.Shared.Preferences.Loadouts.Effects;
using Robust.Shared.Player;
using Robust.Shared.Utility;

namespace Content.Shared._Orehum.Loadouts;

/// <summary>
/// Checks for a specific player GUID.
/// </summary>
public sealed partial class PlayerGUIDLoadoutEffect : LoadoutEffect
{
    private Guid? _guid;

    [DataField(required: true)]
    public string Guid;

    public override bool Validate(HumanoidCharacterProfile profile,
        RoleLoadout loadout,
        ICommonSession? session,
        IDependencyCollection collection,
        [NotNullWhen(false)] out FormattedMessage? reason)
    {
        if (session == null)
        {
            reason = FormattedMessage.Empty;
            return false;
        }

        _guid ??= new Guid(Guid);

        if (session.UserId == _guid)
        {
            reason = null;
            return true;
        }

        reason = FormattedMessage.FromUnformatted(Loc.GetString("loadout-group-player-restriction"));
        return false;
    }
}
