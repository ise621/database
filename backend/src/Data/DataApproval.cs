using System;
using Microsoft.EntityFrameworkCore;
using Database.Data;

namespace Database.Data;

[Owned]
public sealed class DataApproval
    : IApproval
{
    public DataApproval(
        DateTime timestamp,
        string signature,
        string keyFingerprint,
        string query,
        string response,
        Guid approverId,
        IReference statement
    )
    {
        Timestamp = timestamp;
        Signature = signature;
        KeyFingerprint = keyFingerprint;
        Query = query;
        Response = response;
        ApproverId = approverId;
        Statement = statement;
    }

    public Guid ApproverId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string Signature { get; private set; }
    public string KeyFingerprint { get; private set; }
    public string Query { get; private set; }
    public string Response { get; private set; }
    public IReference Statement { get; private set; }
}