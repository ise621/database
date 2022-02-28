using Guid = System.Guid;

namespace Database.Data
{
    public interface IEntity
    {
        Guid Id { get; }

        uint xmin { get; } // https://www.npgsql.org/efcore/modeling/concurrency.html
    }
}