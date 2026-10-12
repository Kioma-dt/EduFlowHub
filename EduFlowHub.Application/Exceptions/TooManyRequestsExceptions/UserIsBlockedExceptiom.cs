namespace EduFlowHub.Application.Exceptions.TooManyRequestsExceptions;

public class UserIsBlockedExceptiom
    : TooManyRequestsException
{
    public TimeSpan TryAfter { get; private set; }
    public UserIsBlockedExceptiom(TimeSpan tryAfter)
        : base($"User has beem blocked. Try after: {tryAfter.TotalSeconds} seconds.")
    {
        TryAfter =  tryAfter;
    }
}