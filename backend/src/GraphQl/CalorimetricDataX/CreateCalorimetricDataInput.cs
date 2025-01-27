using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.CalorimetricDataX;

public sealed record CreateCalorimetricDataInput(
    // TODO Why does specifying the type with an attribute not work here?
    [GraphQLType<NonNullType<LocaleType>>] string Locale,
    Guid ComponentId,
    string? Name,
    string? Description,
    string[] Warnings,
    DateTime CreatedAt,
    Guid CreatorId,
    AppliedMethodInput AppliedMethod,
    IReadOnlyList<DataApprovalInput> Approvals,
    // ResponseApproval Approval
    RootGetHttpsResourceInput RootResource,
    double[] GValues,
    double[] UValues
);