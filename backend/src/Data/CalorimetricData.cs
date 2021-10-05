// using System;
// using System.Collections.Generic;

// namespace Database.Data
// {
//     public sealed class CalorimetricData
//     : Data
//     {
//         public CalorimetricData(
//           DateTime timestamp,
//           string locale,
//           Guid databaseId,
//           Guid componentId,
//           string? name,
//           string? description,
//           string[] warnings,
//           Guid creatorId,
//           DateTime createdAt,
//           AppliedMethod appliedMethod,
//           ICollection<GetHttpsResource> resources,
//           GetHttpsResourceTree resourceTree,
//           ICollection<DataApproval> approvals,
//           // ResponseApproval approval
//           double[] gValues,
//           double[] uValues
//         ) : base(
//           timestamp: timestamp,
//           locale: locale,
//           databaseId: databaseId,
//           componentId: componentId,
//           name: name,
//           description: description,
//           warnings: warnings,
//           creatorId: creatorId,
//           createdAt: createdAt,
//           appliedMethod: appliedMethod,
//           resources: resources,
//           resourceTree: resourceTree,
//           approvals: approvals
//         )
//         {
//             GValues = gValues;
//             UValues = uValues;
//         }

//         public double[] GValues { get; private set; }
//         public double[] UValues { get; private set; }
//     }

// }
