using System.Collections.Generic;

namespace Database.GraphQl.GetHttpsResources
{
    public abstract class GetHttpsResourcePayload<TGetHttpsResourceError>
      : Payload
      where TGetHttpsResourceError : IUserError
    {
        public Data.GetHttpsResource? GetHttpsResource { get; }
        public IReadOnlyCollection<TGetHttpsResourceError>? Errors { get; }

        protected GetHttpsResourcePayload(
            Data.GetHttpsResource getHttpsResource
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
            Data.GetHttpsResource getHttpsResource,
            IReadOnlyCollection<TGetHttpsResourceError> errors
            )
        {
            GetHttpsResource = getHttpsResource;
            Errors = errors;
        }

        protected GetHttpsResourcePayload(
            Data.GetHttpsResource getHttpsResource,
            TGetHttpsResourceError error
            )
          : this(
              getHttpsResource,
              new[] { error }
              )
        {
        }
    }
}
