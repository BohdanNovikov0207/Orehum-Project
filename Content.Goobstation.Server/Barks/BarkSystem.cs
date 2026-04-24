using Content.Goobstation.Common.Barks;
using Content.Goobstation.Common.CCVar;
using Content.Server.Chat.Systems;
using Robust.Shared.Configuration;
using Robust.Shared.Player;

namespace Content.Goobstation.Server.Barks;

public sealed class BarkSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _configurationManager = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SpeechSynthesisComponent, EntitySpokeEvent>(OnEntitySpoke);
    }

    private void OnEntitySpoke(EntityUid uid, SpeechSynthesisComponent comp, EntitySpokeEvent args)
    {
        if (comp.VoicePrototypeId is null
            || !args.Language.SpeechOverride.RequireSpeech
            || !_configurationManager.GetCVar(GoobCVars.BarksEnabled))
            return;

        var sourceEntity = GetNetEntity(uid);
        RaiseNetworkEvent(new PlayBarkEvent(sourceEntity, args.Message, args.IsWhisper), Filter.Pvs(uid));
    }
}
