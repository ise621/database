using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    public sealed class DataType
      : ObjectType<Data.DataX>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.DataX> descriptor
            )
        {
            // `..^1` is a range as introduced in https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#indices-and-ranges
            descriptor.Name(nameof(Data.DataX)[..^1]);
        }
    }
}
