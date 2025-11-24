using NUnit.Framework;
using System.Xml.Linq;
using Unity.VisualScripting;
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
    public float gameScore = 0;
    public float timeToWait = 3;

    private GameObject stopwatchObj;
    private GameObject distanceTrackerObj;
    private GameObject hitByCar;
    private int distanceRan;
    private bool waitToEnd;
    private float timer;

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

        waitToEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }

        if (waitToEnd)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameObject.Find("SceneManager").GetComponent<MenuButtonEvents>().EndGame();
            }
        }
    }

    public void EndGame()
    {
        if (stopwatchObj != null)
        {
            player.GetComponent<Movement>().FreezeMovement();
            stopwatchObj.GetComponent<StopWatch>().StopTimer();
            ScoreTracker.CalcTimeScore(stopwatchObj.GetComponent<StopWatch>().currentMin * 60f + stopwatchObj.GetComponent<StopWatch>().currentSec + stopwatchObj.GetComponent<StopWatch>().currentMs / 100f);
            gameScore = (float)ScoreTracker.GetFinalScore() * ((float)distanceTrackerObj.GetComponent<DistanceTracker>().currentTotalDistance / (finishDistance * distanceTrackerObj.GetComponent<DistanceTracker>().realworldDistanceOfChunk));
            distanceRan = distanceTrackerObj.GetComponent<DistanceTracker>().currentTotalDistance;

            waitToEnd = true;
            timer = timeToWait;
        }
    }

    public void SpawnNPCWhenScrolling(int min, int sec)
    {
        if (!scrollEnvironment.GetComponent<ScrollEnvironment>().IsPaused())
        {
            GetComponent<NPCSpawner>().SpawnNPC(min, sec);
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
        endResults.GetComponent<EndResults>().UpdateResults(distanceRan, FinalMin(), FinalSec(), Mathf.RoundToInt(FinalMs()), Mathf.RoundToInt(gameScore));
    }
}
