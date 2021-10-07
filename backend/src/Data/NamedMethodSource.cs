using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class NamedMethodSource
    {
        public string Name { get; private set; }
        public CrossDatabaseDataReference Value { get; private set; } = default!;

        public NamedMethodSource(
            string name,
            CrossDatabaseDataReference value
        )
        : this(name: name)
        {
            Name = name;
            Value = value;
        }

        // `DbContext` needs this constructor without owned entities.
        public NamedMethodSource(
            string name
        )
        {
            Name = name;
        }
    }
}