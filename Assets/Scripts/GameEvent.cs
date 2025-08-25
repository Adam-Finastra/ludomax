using System;

public static class GameEvent
{
    public static Action EnableButton;
    public static Action<string> OnTeamFinished;

    public static void EnableButtonNow() => EnableButton?.Invoke();
    public static void TeamFinished(string teamName) => OnTeamFinished?.Invoke(teamName); 
}
