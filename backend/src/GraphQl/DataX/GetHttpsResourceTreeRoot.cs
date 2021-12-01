using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeRoot : IGetHttpsResourceTreeVertex
    {
        [GraphQLType(typeof(NonNullType<IdType>))]
        public string VertexId
        {
            get => Data.GetHttpsResource.ConstructVertexId(Value.Id);
        }

        public Data.GetHttpsResource Value { get; }

        public GetHttpsResourceTreeRoot(
            Data.GetHttpsResource value
        )
        {
            Value = value;
        }
    }
}
