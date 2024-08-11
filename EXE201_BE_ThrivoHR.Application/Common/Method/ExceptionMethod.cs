namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class ExceptionMethod
{
    public static string GetKeyString(string ex)
    {
        var keystring = ex.ToString();
        var start = keystring.IndexOf("IX_AppUsers_");
        var end = keystring.IndexOf("'.");
        return keystring[start..end];
    }
}
