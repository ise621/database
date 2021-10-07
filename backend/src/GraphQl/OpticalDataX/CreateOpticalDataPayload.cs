namespace Database.GraphQl.OpticalDataX
{
    public sealed class CreateOpticalDataPayload
      : OpticalDataPayload<CreateOpticalDataError>
    {
        public CreateOpticalDataPayload(
            Data.OpticalData opticalData
            )
              : base(opticalData)
        {
        }

        public CreateOpticalDataPayload(
          CreateOpticalDataError error
        )
        : base(error)
        {
        }
    }
}
