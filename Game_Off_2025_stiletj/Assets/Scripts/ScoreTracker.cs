using JetBrains.Annotations;
using UnityEngine;

public static class ScoreTracker
{
    [SerializeField] private static float timeScore = 0;
    [SerializeField] private static  int interactionScore = 0;
    private static float maxTimeScore = 10000;
    private static int totalScore = 0;
    private static int pointPerInteract = 10;

    public static void IncrementInteractionScore()
    {
        interactionScore++;
    }

    public static float CalcTimeScore(float time)
    {
        timeScore = maxTimeScore - time * 15;
        return timeScore;
    }

    public static int GetFinalScore()
    {
        totalScore = Mathf.RoundToInt(timeScore) + pointPerInteract * interactionScore;
        return totalScore;
    }

    public static void ResetScore()
    {
        timeScore = 0;
        interactionScore = 0;
    }
}
