namespace Database.GraphQl.PhotovoltaicDataX
{
    public sealed class CreatePhotovoltaicDataPayload
      : PhotovoltaicDataPayload<CreatePhotovoltaicDataError>
    {
        public CreatePhotovoltaicDataPayload(
            Data.PhotovoltaicData photovoltaicData
            )
              : base(photovoltaicData)
        {
        }

        public CreatePhotovoltaicDataPayload(
          CreatePhotovoltaicDataError error
        )
        : base(error)
        {
        }
    }
}
