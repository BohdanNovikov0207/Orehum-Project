namespace Content.Shared.Speech;

public sealed class AccentGetEvent : EntityEventArgs
{
    public AccentGetEvent(EntityUid entity, string message)
    {
        Entity = entity;
        Message = message;
    }

    /// <summary>
    /// The entity to apply the accent to.
    /// </summary>
    public EntityUid Entity { get; }

    /// <summary>
    /// The message to apply the accent transformation to.
    /// Modify this to apply the accent.
    /// </summary>
    public string Message { get; set; }
}
