using System.Collections.Generic;

namespace Database.GraphQl.HygrothermalDataX
{
    public abstract class HygrothermalDataPayload<THygrothermalDataError>
      : Payload
      where THygrothermalDataError : IUserError
    {
        public Data.HygrothermalData? HygrothermalData { get; }
        public IReadOnlyCollection<THygrothermalDataError>? Errors { get; }

        protected HygrothermalDataPayload(
            Data.HygrothermalData hygrothermalData
            )
        {
            HygrothermalData = hygrothermalData;
        }

        protected HygrothermalDataPayload(
            IReadOnlyCollection<THygrothermalDataError> errors
            )
        {
            Errors = errors;
        }

        protected HygrothermalDataPayload(
            THygrothermalDataError error
            )
          : this(new[] { error })
        {
        }

        protected HygrothermalDataPayload(
            Data.HygrothermalData hygrothermalData,
            IReadOnlyCollection<THygrothermalDataError> errors
            )
        {
            HygrothermalData = hygrothermalData;
            Errors = errors;
        }

        protected HygrothermalDataPayload(
            Data.HygrothermalData hygrothermalData,
            THygrothermalDataError error
            )
          : this(
              hygrothermalData,
              new[] { error }
              )
        {
        }
    }
}
