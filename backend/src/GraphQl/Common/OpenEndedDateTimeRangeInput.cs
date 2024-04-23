using DateTime = System.DateTime;

namespace Database.GraphQl.Common;

public sealed record OpenEndedDateTimeRangeInput(
    DateTime? From,
    DateTime? Until
);