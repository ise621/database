namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeRoot : IGetHttpsResourceTreeVertex
    {
        public string VertexId { get; }
        public Data.GetHttpsResource Value { get; }

        public GetHttpsResourceTreeRoot(
            Data.GetHttpsResource value
        )
        {
            // TODO base64 encode `VertexId`.
            VertexId = value.Id.ToString("D");
            Value = value;
        }
    }
}
