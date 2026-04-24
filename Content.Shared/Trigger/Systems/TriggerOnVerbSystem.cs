using Content.Shared.Trigger.Components.Triggers;
using Content.Shared.Verbs;

namespace Content.Shared.Trigger.Systems;

public sealed class TriggerOnVerbSystem : EntitySystem
{
    [Dependency] private readonly TriggerSystem _trigger = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TriggerOnVerbComponent, GetVerbsEvent<AlternativeVerb>>(OnGetAltVerbs);
    }

    private void OnGetAltVerbs(Entity<TriggerOnVerbComponent> ent, ref GetVerbsEvent<AlternativeVerb> args)
    {
        if (!args.CanInteract || !args.CanAccess || args.Hands == null)
            return;

        var user = args.User;

        args.Verbs.Add(new AlternativeVerb
        {
            Text = Loc.GetString(ent.Comp.Text),
            Act = () => _trigger.Trigger(ent.Owner, user, ent.Comp.KeyOut),
            Priority = 2, // should be above any timer settings
        });
    }
}
