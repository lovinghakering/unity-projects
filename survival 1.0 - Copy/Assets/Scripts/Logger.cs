using UnityEngine;

public static class Logger
{
    public static void Log(string msg)
    {
        Debug.Log(msg);
        DebugMenu.instance?.PromptMessage(msg);
    }

    public static void LogWarning(string msg)
    {
        Debug.LogWarning(msg);
        DebugMenu.instance?.PromptWarning(msg);
    }

    public static void LogError(string msg)
    {
        Debug.LogError(msg);
        DebugMenu.instance?.PromptError(msg);
    }
}
