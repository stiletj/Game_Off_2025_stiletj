using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject stopwatchPrefab;
    public GameObject distanceTrackerPrefab;
    public ScrollEnvironment scrollEnvironment;
    public int finishDistance = 75;

    private GameObject stopwatchObj;
    private GameObject distanceTrackerObj;
    [SerializeField] private int gameScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopwatchObj = Instantiate(stopwatchPrefab);
        stopwatchObj.transform.SetParent(canvas.transform, false);
        stopwatchObj.GetComponent<StopWatch>().StartTimer();

        distanceTrackerObj = Instantiate(distanceTrackerPrefab);
        distanceTrackerObj.transform.SetParent(canvas.transform, false);
        distanceTrackerObj.GetComponent<DistanceTracker>().SetScrollEnvironment(scrollEnvironment);
        distanceTrackerObj.GetComponent<DistanceTracker>().SetFinishDistance(finishDistance);

        ScoreTracker.ResetScore();

        //stopwatchPrefab.GetComponent<StopWatch>().SetOnSecondFunc(scrollEnvironment.gameObject.GetComponent<NPCSpawner>().SpawnNPC);
        OnTickFunc secFunc = new OnTickFunc(SpawnNPCWhenScrolling);
        stopwatchObj.GetComponent<StopWatch>().SetOnSecondFunc(secFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollEnvironment.distance == finishDistance)
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

    public void SpawnNPCWhenScrolling(int min, int sec)
    {
        if (!scrollEnvironment.GetComponent<ScrollEnvironment>().IsPaused())
        {
            scrollEnvironment.gameObject.GetComponent<NPCSpawner>().SpawnNPC(min, sec);
        }
    }

    public void Test(int min, int sec)
    {
        Debug.Log("Test");
    }
}
