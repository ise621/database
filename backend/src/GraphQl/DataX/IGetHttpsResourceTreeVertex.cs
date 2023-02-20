using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    [InterfaceType("GetHttpsResourceTreeVertex")]
    public interface IGetHttpsResourceTreeVertex
    {
        [GraphQLType<IdType>]
        string VertexId { get; }

        Data.GetHttpsResource Value { get; }
    }
}
