using System;

public static class ErrorHandling
{
    public static void HandleErrorByThrowingException() =>
        throw new Exception("Generic error.");

    public static int? HandleErrorByReturningNullableType(string input)
    {
        if (int.TryParse(input, out var result))
            return result;
        
        return null;
    }

    public static bool HandleErrorWithOutParam(string input, out int result) =>
        int.TryParse(input, out result);

    public static void DisposableResourcesAreDisposedWhenExceptionIsThrown(IDisposable disposableObject)
    {
        using (disposableObject)
            throw new Exception("Dispose error.");
    }
}
