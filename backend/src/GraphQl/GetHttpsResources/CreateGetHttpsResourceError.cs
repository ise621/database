using System.Collections.Generic;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class CreateGetHttpsResourceError
      : GraphQl.UserErrorBase<CreateGetHttpsResourceErrorCode>
    {
        public CreateGetHttpsResourceError(
            CreateGetHttpsResourceErrorCode code,
            string message,
            IReadOnlyList<string> path
            )
          : base(code, message, path)
        {
        }
    }
}
