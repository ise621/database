using System;

namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeNonRootVertex
    : IGetHttpsResourceTreeVertex
    {
        public string VertexId { get; }
        public Data.GetHttpsResource Value { get; }
        public string ParentId { get; }
        public Data.ToTreeVertexAppliedConversionMethod AppliedConversionMethod { get; }

        public GetHttpsResourceTreeNonRootVertex(
            Data.GetHttpsResource value
        )
        {
            // TODO base64 encode `VertexId`.
            VertexId = value.Id.ToString("D");
            Value = value;
            // TODO base64 encode `ParentId`.
            ParentId = (value.ParentId ?? throw new InvalidOperationException("Each non-root vertex has a parent.")).ToString("D");
            AppliedConversionMethod = value.AppliedConversionMethod ?? throw new InvalidOperationException("Each non-root vertex has an applied conversion method.");
        }
    }
}
