namespace Content.Server.Speech;

[ByRefEvent]
public record struct ListenEvent(string Message, EntityUid Source);

[ByRefEvent]
public record struct ListenAttemptEvent(EntityUid Source, bool Cancelled = false)
{
    public void Cancel() => Cancelled = true;

    public void Uncancel() => Cancelled = false;
}
