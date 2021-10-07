using System.Collections.Generic;

namespace Database.GraphQl.OpticalDataX
{
    public abstract class OpticalDataPayload<TOpticalDataError>
      : Payload
      where TOpticalDataError : IUserError
    {
        public Data.OpticalData? OpticalData { get; }
        public IReadOnlyCollection<TOpticalDataError>? Errors { get; }

        protected OpticalDataPayload(
            Data.OpticalData opticalData
            )
        {
            OpticalData = opticalData;
        }

        protected OpticalDataPayload(
            IReadOnlyCollection<TOpticalDataError> errors
            )
        {
            Errors = errors;
        }

        protected OpticalDataPayload(
            TOpticalDataError error
            )
          : this(new[] { error })
        {
        }

        protected OpticalDataPayload(
            Data.OpticalData opticalData,
            IReadOnlyCollection<TOpticalDataError> errors
            )
        {
            OpticalData = opticalData;
            Errors = errors;
        }

        protected OpticalDataPayload(
            Data.OpticalData opticalData,
            TOpticalDataError error
            )
          : this(
              opticalData,
              new[] { error }
              )
        {
        }
    }
}
