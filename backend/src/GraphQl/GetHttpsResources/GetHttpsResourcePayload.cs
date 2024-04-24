using System.Collections.Generic;
using Database.Data;

namespace Database.GraphQl.GetHttpsResources;

public abstract class GetHttpsResourcePayload<TGetHttpsResourceError>
    : Payload
    where TGetHttpsResourceError : IUserError
{
    protected GetHttpsResourcePayload(
        GetHttpsResource getHttpsResource
    )
    {
        GetHttpsResource = getHttpsResource;
    }

    protected GetHttpsResourcePayload(
        IReadOnlyCollection<TGetHttpsResourceError> errors
    )
    {
        Errors = errors;
    }

    protected GetHttpsResourcePayload(
        TGetHttpsResourceError error
    )
        : this(new[] { error })
    {
    }

    protected GetHttpsResourcePayload(
        GetHttpsResource getHttpsResource,
        IReadOnlyCollection<TGetHttpsResourceError> errors
    )
    {
        GetHttpsResource = getHttpsResource;
        Errors = errors;
    }

    protected GetHttpsResourcePayload(
        GetHttpsResource getHttpsResource,
        TGetHttpsResourceError error
    )
        : this(
            getHttpsResource,
            new[] { error }
        )
    {
    }

    public GetHttpsResource? GetHttpsResource { get; }
    public IReadOnlyCollection<TGetHttpsResourceError>? Errors { get; }
}