using System;

namespace Database.GraphQl
{
    public sealed record DataApprovalInput(
          DateTime Timestamp,
          string Signature,
          string KeyFingerprint,
          string Query,
          string Response,
          Guid ApproverId
    );
}