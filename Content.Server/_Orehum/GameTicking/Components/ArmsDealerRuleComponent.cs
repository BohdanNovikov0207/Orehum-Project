using Content.Shared.Random;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Content.Server._Orehum.GameTicking.Rules;

namespace Content.Server._Orehum.GameTicking.Components;

[RegisterComponent, Access(typeof(ArmsDealerRuleSystem))]
public sealed partial class ArmsDealerRuleComponent : Component;
