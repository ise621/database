using System;

namespace Database.GraphQl
{
    public record DataApprovalInput(
          DateTime Timestamp,
          string Signature,
          string KeyFingerprint,
          string Query,
          string Response,
          Guid ApproverId
    );
}