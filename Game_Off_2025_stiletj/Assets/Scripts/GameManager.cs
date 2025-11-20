using NUnit.Framework;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject stopwatchPrefab;
    public GameObject distanceTrackerPrefab;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject endResults;
    public ScrollEnvironment scrollEnvironment;
    public int finishDistance = 75;
    public int gameScore = 0;

    private GameObject stopwatchObj;
    private GameObject distanceTrackerObj;

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

        OnTickFunc secFunc = new OnTickFunc(SpawnNPCWhenScrolling);
        stopwatchObj.GetComponent<StopWatch>().SetOnSecondFunc(secFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void EndGame()
    {
        if (stopwatchObj != null)
        {
            player.GetComponent<Movement>().FreezeMovement();
            stopwatchObj.GetComponent<StopWatch>().StopTimer();
            ScoreTracker.CalcTimeScore(stopwatchObj.GetComponent<StopWatch>().currentMin * 60f + stopwatchObj.GetComponent<StopWatch>().currentSec + stopwatchObj.GetComponent<StopWatch>().currentMs / 100f);
            gameScore = ScoreTracker.GetFinalScore();
        }
    }

    public void SpawnNPCWhenScrolling(int min, int sec)
    {
        if (!scrollEnvironment.GetComponent<ScrollEnvironment>().IsPaused())
        {
            scrollEnvironment.gameObject.GetComponent<NPCSpawner>().SpawnNPC(min, sec);
        }
    }

    public void PauseGame()
    {
        player.GetComponent<Movement>().FreezeMovement();
        scrollEnvironment.GetComponent<ScrollEnvironment>().Pause();
        stopwatchObj.GetComponent<StopWatch>().StopTimer();
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        player.GetComponent<Movement>().UnFreezeMovement();
        scrollEnvironment.GetComponent<ScrollEnvironment>().Play();
        stopwatchObj.GetComponent<StopWatch>().StartTimer();
        pauseMenu.SetActive(false);
    }

    public int FinalMin()
    {
        return stopwatchObj.GetComponent<StopWatch>().currentMin;
    }

    public int FinalSec()
    {
        return stopwatchObj.GetComponent<StopWatch>().currentSec;
    }

    public float FinalMs()
    {
        return stopwatchObj.GetComponent<StopWatch>().currentMs;
    }

    public void ShowEndResults()
    {
        endResults.SetActive(true);
        endResults.GetComponent<EndResults>().UpdateResults(FinalMin(), FinalSec(), Mathf.RoundToInt(FinalMs()), gameScore);
    }
}
