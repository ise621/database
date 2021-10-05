using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class NamedMethodSource
    {
        public NamedMethodSource(
            string name,
            CrossDatabaseDataReference value
        )
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public CrossDatabaseDataReference Value { get; private set; }
    }
}