using Content.Shared.Access;
using Content.Shared.Radio;
using Robust.Shared.Prototypes;

namespace Content.Shared.Cargo.Prototypes;

/// <summary>
/// This is a prototype for a single account that stores money on StationBankAccountComponent
/// </summary>
[Prototype]
public sealed class CargoAccountPrototype : IPrototype
{
    /// <summary>
    /// Paper prototype used for acquisition slips.
    /// </summary>
    [DataField]
    public EntProtoId AcquisitionSlip;

    /// <summary>
    /// A shortened code used to refer to the account in UIs
    /// </summary>
    [DataField]
    public LocId Code;

    /// <summary>
    /// Color corresponding to the account.
    /// </summary>
    [DataField]
    public Color Color;

    /// <summary>
    /// Название для отдела, куда будет указывать доставка по умолчанию.
    /// </summary>
    [DataField]
    public LocId? DepartmentDestinationName;

    /// <summary>
    /// Full IC name of the account.
    /// </summary>
    [DataField]
    public LocId Name;

    /// <summary>
    /// Channel used for announcing transactions.
    /// </summary>
    [DataField]
    public ProtoId<RadioChannelPrototype> RadioChannel;

    // CorvaxGoob-CargoFeatures-Start
    /// <summary>
    /// Доступ, который будет проверяться на возможность установки на ящик, и который будет устанавливаться в случае заказа
    /// такого.
    /// </summary>
    [DataField]
    public HashSet<ProtoId<AccessLevelPrototype>> SecureCrateOrderAccess = new();

    /// <summary>
    /// Прототип для ящика, который будет спавнится при одобрении заказа с пометкой о защите заказа.
    /// </summary>
    [DataField]
    public EntProtoId? SecureCratePrototype;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
    // CorvaxGoob-CargoFeatures-End
}
