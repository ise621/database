namespace Database.GraphQl.CalorimetricDataX
{
    public sealed class CreateCalorimetricDataPayload
      : CalorimetricDataPayload<CreateCalorimetricDataError>
    {
        public CreateCalorimetricDataPayload(
            Data.CalorimetricData calorimetricData
            )
              : base(calorimetricData)
        {
        }

        public CreateCalorimetricDataPayload(
          CreateCalorimetricDataError error
        )
        : base(error)
        {
        }
    }
}
