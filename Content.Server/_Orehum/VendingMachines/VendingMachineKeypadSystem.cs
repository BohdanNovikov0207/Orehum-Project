using Content.Shared._Orehum.VendingMachines;
using Content.Shared.VendingMachines;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;

namespace Content.Server._Orehum.VendingMachines;

public sealed partial class VendingMachineKeypadSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = null!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<VendingMachineComponent, VendingMachineKeypadAudioMessage>(OnKeypadAudio);
    }

    private void OnKeypadAudio(EntityUid uid, VendingMachineComponent component, VendingMachineKeypadAudioMessage args)
    {
        var soundPath = args.SoundType switch
        {
            VendingMachineKeypadSound.beep => "/Audio/Machines/Nuke/general_beep.ogg",
            VendingMachineKeypadSound.success => "/Audio/Machines/vending_jingle.ogg",
            VendingMachineKeypadSound.error => "/Audio/Machines/buzz-two.ogg",
            VendingMachineKeypadSound.timeout => "/Audio/Machines/button.ogg",
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(soundPath))
            return;

        var volume = args.SoundType == VendingMachineKeypadSound.timeout ? -6f : -4f;
        var audioParams = new AudioParams().WithVolume(volume).WithPitchScale(args.Pitch);

        _audio.PlayPredicted(new SoundPathSpecifier(soundPath), uid, args.Actor, audioParams);
    }
}
