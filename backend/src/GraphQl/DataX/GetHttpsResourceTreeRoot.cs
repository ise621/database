using Database.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX;

public sealed class GetHttpsResourceTreeRoot : IGetHttpsResourceTreeVertex
{
    public GetHttpsResourceTreeRoot(
        GetHttpsResource value
    )
    {
        Value = value;
    }

    [GraphQLType<NonNullType<IdType>>] public string VertexId => GetHttpsResource.ConstructVertexId(Value.Id);

    public GetHttpsResource Value { get; }
}