using FluentResults;

namespace Vesma.Api.Common;

public static class Extensions
{
    public static Dictionary<string, string[]> ToActionResult(this ResultBase result) =>
        result.Errors.ToDictionary(k => k.Message, v => v.Reasons.Select(x => x.Message).ToArray());
}
