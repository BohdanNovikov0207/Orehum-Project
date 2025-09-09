namespace Content.Shared.Speech
{
    [ByRefEvent]
    public record struct SpeakAttemptEvent(EntityUid Uid, bool Cancelled = false)
    {
        /// <summary>
        ///     Cancels the event.
        /// </summary>
        public void Cancel() => Cancelled = true;

        /// <summary>
        ///     Uncancels the event. Don't call this unless you know what you're doing.
        /// </summary>
        public void Uncancel() => Cancelled = false;
    }
}
