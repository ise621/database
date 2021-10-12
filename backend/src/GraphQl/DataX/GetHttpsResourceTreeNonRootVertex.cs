using System;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeNonRootVertex
    : IGetHttpsResourceTreeVertex
    {
        [GraphQLType(typeof(NonNullType<IdType>))]
        public string VertexId { get; }

        public Data.GetHttpsResource Value { get; }

        [GraphQLType(typeof(NonNullType<IdType>))]
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
