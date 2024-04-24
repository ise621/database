using HotChocolate.Data.Filters;
using HotChocolate.Types;

namespace Database.GraphQl.Filters;

public interface INotField
    : IInputField
        , IHasRuntimeType
{
    new IFilterInputType DeclaringType { get; }
}