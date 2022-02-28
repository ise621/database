using System.Collections.Generic;

namespace Database.GraphQl.CalorimetricDataX
{
    public abstract class CalorimetricDataPayload<TCalorimetricDataError>
      : Payload
      where TCalorimetricDataError : IUserError
    {
        public Data.CalorimetricData? CalorimetricData { get; }
        public IReadOnlyCollection<TCalorimetricDataError>? Errors { get; }

        protected CalorimetricDataPayload(
            Data.CalorimetricData calorimetricData
            )
        {
            CalorimetricData = calorimetricData;
        }

        protected CalorimetricDataPayload(
            IReadOnlyCollection<TCalorimetricDataError> errors
            )
        {
            Errors = errors;
        }

        protected CalorimetricDataPayload(
            TCalorimetricDataError error
            )
          : this(new[] { error })
        {
        }

        protected CalorimetricDataPayload(
            Data.CalorimetricData calorimetricData,
            IReadOnlyCollection<TCalorimetricDataError> errors
            )
        {
            CalorimetricData = calorimetricData;
            Errors = errors;
        }

        protected CalorimetricDataPayload(
            Data.CalorimetricData calorimetricData,
            TCalorimetricDataError error
            )
          : this(
              calorimetricData,
              new[] { error }
              )
        {
        }
    }
}
