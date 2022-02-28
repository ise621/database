using DateTime = System.DateTime;

namespace Database.GraphQl.Common
{
    public record OpenEndedDateTimeRangeInput(
          DateTime? From,
          DateTime? Until
        );
}