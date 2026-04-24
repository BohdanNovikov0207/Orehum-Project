using System.Numerics;

namespace Content.Shared._Orehum.Weapon.Tracer.Components;

[RegisterComponent]
public sealed partial class TracerComponent : Component
{
    /// <summary>
    /// Color of the tracer line effect
    /// </summary>
    [DataField]
    public Color Color = Color.Red;

    [ViewVariables]
    public TracerData Data = default!;

    /// <summary>
    /// The maximum length of the tracer trail
    /// </summary>
    [DataField]
    public float Length = 2f;

    [DataField]
    public float Lifetime = 10f;
}

[DataRecord]
public sealed class TracerData(List<Vector2> positionHistory, TimeSpan endTime)
{
    /// <summary>
    /// When this tracer effect should end
    /// </summary>
    public TimeSpan EndTime = endTime;

    public List<Vector2> PositionHistory = positionHistory;
}
