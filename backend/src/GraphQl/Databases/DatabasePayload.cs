using System.Collections.Generic;

namespace Database.GraphQl.Databases
{
    public abstract class DatabasePayload<TDatabaseError>
      : Payload
      where TDatabaseError : IUserError
    {
        public Database? Database { get; }
        public IReadOnlyCollection<TDatabaseError>? Errors { get; }

        protected DatabasePayload(
            Database database
            )
        {
            Database = database;
        }

        protected DatabasePayload(
            IReadOnlyCollection<TDatabaseError> errors
            )
        {
            Errors = errors;
        }

        protected DatabasePayload(
            TDatabaseError error
            )
          : this(new[] { error })
        {
        }

        protected DatabasePayload(
            Database database,
            IReadOnlyCollection<TDatabaseError> errors
            )
        {
            Database = database;
            Errors = errors;
        }

        protected DatabasePayload(
            Database database,
            TDatabaseError error
            )
          : this(
              database,
              new[] { error }
              )
        {
        }
    }
}