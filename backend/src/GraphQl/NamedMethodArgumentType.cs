using HotChocolate.Types;

namespace Database.GraphQl
{
    public sealed class NamedMethodArgumentType
      : ObjectType<Data.NamedMethodArgument>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.NamedMethodArgument> descriptor
            )
        {
            // TODO Convert value to "nested collections", that is, some
            // hierarchy of `IReadOnlyDictionary<string, object>` and
            // `IReadOnlyList<object>`. See also
            // https://github.com/ChilliCream/hotchocolate/issues/3661 and
            // https://github.com/ChilliCream/hotchocolate/pull/3713
            // The latter contains code of how to do such a conversion.
            descriptor
                .Field(x => x.Value)
                .Type<NonNullType<AnyType>>()
                .Resolve(context =>
                    "TODO"
                // context.Parent<Data.NamedMethodArgument>()
                // .Value
                );
        }

        // TODO Below is some code I wrote to convet `JsonValue` to "nested collections". It is based on Newtonsoft's JSON representation though.
        // private static object? ConvertJsonValueToNestedCollections(JsonValue jsonValue)
        // {
        //     return jsonValue.Type switch
        //     {
        //         JsonValueType.Array =>
        //           jsonValue.Array
        //           .Select(ConvertJsonValueToNestedCollections)
        //           .ToList().AsReadOnly(),
        //         JsonValueType.Boolean => jsonValue.Boolean,
        //         JsonValueType.Null => null,
        //         JsonValueType.Number => jsonValue.Number,
        //         JsonValueType.Object =>
        //           new ReadOnlyDictionary<string, object?>(
        //               jsonValue.Object.ToDictionary(
        //                 pair => pair.Key,
        //                 pair => ConvertJsonValueToNestedCollections(pair.Value)
        //                 )
        //               ),
        //         JsonValueType.String => jsonValue.String,
        //         // God-damned C# does not have switch expression exhaustiveness for
        //         // enums as mentioned for example on https://github.com/dotnet/csharplang/issues/2266
        //         _ => throw new Exception($"The JSON value `{jsonValue}` of type `{jsonValue.Type}` fell through")
        //     };
        // }

        // private static Result<JsonValue, Errors> ConvertNestedCollectionsToJsonValue(
        //     object? jsonValue,
        //     IReadOnlyList<object>? path = null
        //     )
        // {
        //     return jsonValue switch
        //     {
        //         IList<object?> list =>
        //           list.Select((v, index) =>
        //               ConvertNestedCollectionsToJsonValue(
        //                 v,
        //                 path?.Append(index).ToList().AsReadOnly()
        //                 )
        //               )
        //           .Combine()
        //           .Map(jsonValues =>
        //               new JsonValue(
        //                 new JsonArray(jsonValues)
        //                 )
        //               ),
        //         bool boolean => Result.Success<JsonValue, Errors>(new JsonValue(boolean)),
        //         null => Result.Success<JsonValue, Errors>(JsonValue.Null),
        //         // For the list of implicit conversions to `double` see
        //         // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions#implicit-numeric-conversions
        //         // and for the explicit ones see
        //         // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions#explicit-numeric-conversions
        //         sbyte number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         byte number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         short number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         ushort number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         int number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         uint number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         long number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         ulong number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         float number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         double number => Result.Success<JsonValue, Errors>(new JsonValue(number)),
        //         decimal number => Result.Success<JsonValue, Errors>(new JsonValue((double)number)),
        //         IDictionary<string, object?> dictionary =>
        //           dictionary.Select(pair =>
        //               ConvertNestedCollectionsToJsonValue(
        //                 pair.Value,
        //                 path?.Append(pair.Key).ToList().AsReadOnly()
        //                 )
        //               .Map(jsonValue =>
        //                 new KeyValuePair<string, JsonValue>(pair.Key, jsonValue)
        //                 )
        //               )
        //           .Combine()
        //           .Map(stringJsonValuePairs =>
        //               new JsonValue(
        //                 new JsonObject(
        //                   stringJsonValuePairs.ToDictionary(
        //                     pair => pair.Key,
        //                     pair => (JsonValue?)pair.Value
        //                     )
        //                   )
        //                 )
        //               ),
        //         string @string => Result.Success<JsonValue, Errors>(new JsonValue(@string)),
        //         // God-damned C# does not have switch expression exhaustiveness for
        //         // enums as mentioned for example on https://github.com/dotnet/csharplang/issues/2266
        //         _ => Result.Failure<JsonValue, Errors>(
        //             Errors.One(
        //               message: $"The JSON value `{jsonValue}` of type `{jsonValue.GetType()}` fell through",
        //               code: ErrorCodes.InvalidValue,
        //               path: path
        //               )
        //             )
        //     };
        // }

        // private static Result<JsonValue, Errors> ConvertJsonElementToJsonValue(
        //     JsonElement jsonElement,
        //     IReadOnlyList<object>? path = null
        //     )
        // {
        //     return jsonElement.ValueKind switch
        //     {
        //         JsonValueKind.Array =>
        //           jsonElement.EnumerateArray()
        //           .Select((v, index) =>
        //               ConvertJsonElementToJsonValue(
        //                 v,
        //                 path?.Append(index).ToList().AsReadOnly()
        //                 )
        //               )
        //           .Combine()
        //           .Map(jsonValues =>
        //               new JsonValue(
        //                 new JsonArray(jsonValues)
        //                 )
        //               ),
        //         JsonValueKind.False => Result.Success<JsonValue, Errors>(new JsonValue(false)),
        //         JsonValueKind.Null => Result.Success<JsonValue, Errors>(JsonValue.Null),
        //         JsonValueKind.Number => Result.Success<JsonValue, Errors>(new JsonValue(jsonElement.GetDouble())),
        //         JsonValueKind.Object =>
        //           jsonElement.EnumerateObject()
        //           .Select(jsonProperty =>
        //               ConvertJsonElementToJsonValue(
        //                 jsonProperty.Value,
        //                 path?.Append(jsonProperty.Name).ToList().AsReadOnly()
        //                 )
        //               .Map(jsonValue =>
        //                 new KeyValuePair<string, JsonValue>(jsonProperty.Name, jsonValue)
        //                 )
        //               )
        //           .Combine()
        //           .Map(stringJsonValuePairs =>
        //               new JsonValue(
        //                 new JsonObject(
        //                   stringJsonValuePairs.ToDictionary(
        //                     pair => pair.Key,
        //                     pair => (JsonValue?)pair.Value
        //                     )
        //                   )
        //                 )
        //               ),
        //         JsonValueKind.String => Result.Success<JsonValue, Errors>(new JsonValue(jsonElement.GetString())),
        //         JsonValueKind.True => Result.Success<JsonValue, Errors>(new JsonValue(true)),
        //         JsonValueKind.Undefined => Result.Failure<JsonValue, Errors>(
        //             Errors.One(
        //               message: $"The JSON element `{jsonElement}` is of kind `{JsonValueKind.Undefined}`",
        //               code: ErrorCodes.InvalidValue,
        //               path: path
        //               )
        //             ),
        //         // God-damned C# does not have switch expression exhaustiveness for
        //         // enums as mentioned for example on https://github.com/dotnet/csharplang/issues/2266
        //         _ => Result.Failure<JsonValue, Errors>(
        //             Errors.One(
        //               message: $"The JSON element `{jsonElement}` of kind `{jsonElement.ValueKind}` fell through",
        //               code: ErrorCodes.InvalidValue,
        //               path: path
        //               )
        //             )
        //     };
        // }
    }
}
