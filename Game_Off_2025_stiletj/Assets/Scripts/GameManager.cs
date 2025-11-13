using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject stopwatchPrefab;
    public ScrollEnvironment scrollEnvironment;

    private GameObject stopwatchObj;
    [SerializeField] private int gameScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopwatchObj = Instantiate(stopwatchPrefab);
        stopwatchObj.transform.SetParent(canvas.transform, false);
        stopwatchPrefab.GetComponent<StopWatch>().StartTimer();
        ScoreTracker.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollEnvironment.distance == 75)
        {
            EndGame();
            scrollEnvironment.Pause();
        }
    }

    private void EndGame()
    {
        if (stopwatchObj != null)
        {
            stopwatchObj.GetComponent<StopWatch>().StopTimer();
            ScoreTracker.CalcTimeScore(stopwatchObj.GetComponent<StopWatch>().currentMin * 60f + stopwatchObj.GetComponent<StopWatch>().currentSec + stopwatchObj.GetComponent<StopWatch>().currentMs / 100f);
            gameScore = ScoreTracker.GetFinalScore();
            //Destroy(stopwatchObj);
        }
    }
}
