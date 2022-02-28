using System.Collections.Generic;

namespace Database.GraphQl.PhotovoltaicDataX
{
    public abstract class PhotovoltaicDataPayload<TPhotovoltaicDataError>
      : Payload
      where TPhotovoltaicDataError : IUserError
    {
        public Data.PhotovoltaicData? PhotovoltaicData { get; }
        public IReadOnlyCollection<TPhotovoltaicDataError>? Errors { get; }

        protected PhotovoltaicDataPayload(
            Data.PhotovoltaicData photovoltaicData
            )
        {
            PhotovoltaicData = photovoltaicData;
        }

        protected PhotovoltaicDataPayload(
            IReadOnlyCollection<TPhotovoltaicDataError> errors
            )
        {
            Errors = errors;
        }

        protected PhotovoltaicDataPayload(
            TPhotovoltaicDataError error
            )
          : this(new[] { error })
        {
        }

        protected PhotovoltaicDataPayload(
            Data.PhotovoltaicData photovoltaicData,
            IReadOnlyCollection<TPhotovoltaicDataError> errors
            )
        {
            PhotovoltaicData = photovoltaicData;
            Errors = errors;
        }

        protected PhotovoltaicDataPayload(
            Data.PhotovoltaicData photovoltaicData,
            TPhotovoltaicDataError error
            )
          : this(
              photovoltaicData,
              new[] { error }
              )
        {
        }
    }
}
