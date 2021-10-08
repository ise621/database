using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    [InterfaceType("GetHttpsResourceTreeVertex")]
    public interface IGetHttpsResourceTreeVertex
    {
        string VertexId { get; }
        Data.GetHttpsResource Value { get; }
    }
}
