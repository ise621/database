using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

[Owned]
public sealed class NamedMethodArgument
    : IDisposable
{
    public NamedMethodArgument(
        string name,
        JsonDocument value
    )
    {
        Name = name;
        Value = value;
    }

    public string Name { get; private set; }
    public JsonDocument Value { get; }

    public void Dispose()
    {
        Value.Dispose();
    }
}