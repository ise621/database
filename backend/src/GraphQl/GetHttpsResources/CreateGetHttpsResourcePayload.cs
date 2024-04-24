using Database.Data;

namespace Database.GraphQl.GetHttpsResources;

public sealed class CreateGetHttpsResourcePayload
    : GetHttpsResourcePayload<CreateGetHttpsResourceError>
{
    public CreateGetHttpsResourcePayload(
        GetHttpsResource getHttpsResource
    )
        : base(getHttpsResource)
    {
    }

    public CreateGetHttpsResourcePayload(
        CreateGetHttpsResourceError error
    )
        : base(error)
    {
    }
}