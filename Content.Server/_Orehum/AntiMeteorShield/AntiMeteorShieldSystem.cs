using Content.Shared._Orehum.Generic;
using Content.Shared.Damage;
using Content.Shared.Power;

namespace Content.Server._Orehum.AntiMeteorShield;

public sealed class AntiMeteorShieldSystem : EntitySystem
{
    private readonly Dictionary<EntityUid, AntiMeteorShieldGeneratorComponent> _generatorComponents = new(4);

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ProtectedFromMeteorComponent, BeforeDamageChangedEvent>(OnBeforeDamageChangedEvent);

        SubscribeLocalEvent<AntiMeteorShieldGeneratorComponent, ComponentInit>(OnGeneratorInit);
        SubscribeLocalEvent<AntiMeteorShieldGeneratorComponent, ComponentShutdown>(OnGeneratorRemove);

        SubscribeLocalEvent<AntiMeteorShieldGeneratorComponent, PowerChangedEvent>(OnPowerChange);
    }

    public override void Update(float frameTime)
    {
        foreach (var (_, gen) in _generatorComponents)
        {
            if (!gen.Powered)
                continue;

            if (gen.Health >= gen.MaxHealth)
                continue;

            gen.Accumulated += frameTime;

            while (gen.Accumulated >= gen.SecondsToHealOneHp)
            {
                gen.Accumulated -= gen.SecondsToHealOneHp;
                gen.Health = MathF.Min(gen.Health + 1f, gen.MaxHealth);

                if (gen.Shutdown && gen.Health >= gen.EnableOnHealth) // перезагрузка
                    gen.Shutdown = false;
            }
        }
    }

    private void OnPowerChange(EntityUid uid, AntiMeteorShieldGeneratorComponent component, ref PowerChangedEvent args)
    {
        component.Powered = args.Powered;
        //Log.Debug($"now is powered: {component.Powered}");
    }

    private void OnBeforeDamageChangedEvent(EntityUid uid, ProtectedFromMeteorComponent component, ref BeforeDamageChangedEvent args)
    {
        if (args.Cancelled)
            return;

        args.Cancelled = IsBlockedByShield(uid, args.Damage);
    }

    private void OnGeneratorInit(Entity<AntiMeteorShieldGeneratorComponent> ent, ref ComponentInit args)
    {
        if (!TryComp(ent.Owner, out TransformComponent? transformComponent))
            return;

        if (transformComponent.GridUid is not { Valid: true, } gridUid)
            return;

        //Log.Debug($"Registered AntiMeteorShieldGeneratorComponent for grid {gridUid}");
        _generatorComponents[gridUid] = ent.Comp;
    }

    private void OnGeneratorRemove(Entity<AntiMeteorShieldGeneratorComponent> ent, ref ComponentShutdown args)
    {
        foreach (var (uid, gen) in _generatorComponents)
        {
            if (gen == ent.Comp)
            {
                _generatorComponents.Remove(uid);
                //Log.Debug($"Unegistered AntiMeteorShieldGeneratorComponent for grid {uid}");
                return;
            }
        }
    }

    public bool IsBlockedByShield(EntityUid structure, DamageSpecifier damage)
    {
        if (!TryComp(structure, out TransformComponent? transformComponent))
            return false;

        if (transformComponent.GridUid is not { Valid: true, } gridUid)
            return false;

        if (!_generatorComponents.TryGetValue(gridUid, out var shieldGenerator))
            return false;

        if (!shieldGenerator.Powered || shieldGenerator.Shutdown)
            return false;

        //if (!damage.DamageDict.TryGetValue("Structural", out var structDmg))
        //    return false;

        //var dmg = structDmg.Float() / shieldGenerator.DamageReducerModifier;
        var dmg = damage.GetTotal().Float() / shieldGenerator.DamageReducerModifier;

        if (dmg <= shieldGenerator.IgnoreDamage) // скип удары руками щиткод стайл
            return false;

        if (shieldGenerator.Health <= dmg)
            return false;

        var prevHealth = shieldGenerator.Health;
        shieldGenerator.Health -= dmg;

        //Log.Info($"Changed shield hp: {prevHealth} => {shieldGenerator.Health}");

        if (shieldGenerator.Health <= shieldGenerator.ShutdownOnHealth)
            shieldGenerator.Shutdown = true;

        var shield = Spawn("AntiMeteorShieldTriggered", transformComponent.Coordinates);
        if (TryComp(shield, out FadeAndDeleteComponent? fade))
        {
            var blue = prevHealth / shieldGenerator.MaxHealth;
            var red = 1f - blue;
            fade.Modulate = new(red, 1f, blue, 1f);
            Dirty(shield, fade);
        }

        return true;
    }
}
