using System;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeNonRootVertex
    : IGetHttpsResourceTreeVertex
    {
        [GraphQLType<NonNullType<IdType>>]
        public string VertexId
        {
            get => Data.GetHttpsResource.ConstructVertexId(Value.Id);
        }

        public Data.GetHttpsResource Value { get; }

        [GraphQLType<NonNullType<IdType>>]
        public string ParentId
        {
            get => Data.GetHttpsResource.ConstructVertexId(
                Value.ParentId
                ?? throw new InvalidOperationException("Impossible! Each non-root vertex has a parent.")
                );
        }

        public Data.ToTreeVertexAppliedConversionMethod AppliedConversionMethod { get; }

        public GetHttpsResourceTreeNonRootVertex(
            Data.GetHttpsResource value
        )
        {
            Value = value;
            AppliedConversionMethod = value.AppliedConversionMethod ?? throw new InvalidOperationException("Each non-root vertex has an applied conversion method.");
        }
    }
}