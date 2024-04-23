using Database.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX;

[InterfaceType("GetHttpsResourceTreeVertex")]
public interface IGetHttpsResourceTreeVertex
{
    [GraphQLType<IdType>] string VertexId { get; }

    GetHttpsResource Value { get; }
}