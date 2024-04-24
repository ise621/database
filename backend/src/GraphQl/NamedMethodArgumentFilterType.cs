using Database.Data;
using HotChocolate.Data.Filters;

namespace Database.GraphQl;

public sealed class NamedMethodArgumentFilterType
    : FilterInputType<NamedMethodArgument>
{
    protected override void Configure(
        IFilterInputTypeDescriptor<NamedMethodArgument> descriptor
    )
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Name);
        // TODO descriptor.Field(x => x.Value);
    }
}