using System;
using HotChocolate.Types;

namespace Database.Data;

[InterfaceType("Approval")]
public interface IApproval
{
    DateTime Timestamp { get; }
    string Signature { get; }
    string KeyFingerprint { get; }
    string Query { get; }
    string Response { get; }
}