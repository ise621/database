using System;
using Database.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.DataX;

public sealed class GetHttpsResourceTreeNonRootVertex
    : IGetHttpsResourceTreeVertex
{
    public GetHttpsResourceTreeNonRootVertex(
        GetHttpsResource value
    )
    {
        Value = value;
        AppliedConversionMethod = value.AppliedConversionMethod ??
                                  throw new InvalidOperationException(
                                      "Each non-root vertex has an applied conversion method.");
    }

    [GraphQLType<NonNullType<IdType>>]
    public string ParentId =>
        GetHttpsResource.ConstructVertexId(
            Value.ParentId
            ?? throw new InvalidOperationException("Impossible! Each non-root vertex has a parent.")
        );

    public ToTreeVertexAppliedConversionMethod AppliedConversionMethod { get; }

    [GraphQLType<NonNullType<IdType>>] public string VertexId => GetHttpsResource.ConstructVertexId(Value.Id);

    public GetHttpsResource Value { get; }
}