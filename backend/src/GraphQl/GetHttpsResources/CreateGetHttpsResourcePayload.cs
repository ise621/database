namespace Database.GraphQl.GetHttpsResources
{
    public sealed class CreateGetHttpsResourcePayload
      : GetHttpsResourcePayload<CreateGetHttpsResourceError>
    {
        public CreateGetHttpsResourcePayload(
            Data.GetHttpsResource getHttpsResource
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
}
