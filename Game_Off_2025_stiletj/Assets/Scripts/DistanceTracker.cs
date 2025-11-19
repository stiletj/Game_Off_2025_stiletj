using TMPro;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public TextMeshProUGUI distance;
    public int currentTotalDistance;
    public int realworldDistanceOfChunk;

    private ScrollEnvironment scrollEnvironment;
    private float chunkLength;
    private int finishDistance;
    private float startOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTotalDistance = 0;
        chunkLength = scrollEnvironment.front.transform.position.z - scrollEnvironment.back.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceThisChunk = scrollEnvironment.gameObject.transform.position.z - scrollEnvironment.back.transform.position.z;
        float percentageOfChunk = distanceThisChunk / chunkLength;

        currentTotalDistance = scrollEnvironment.distance * realworldDistanceOfChunk + Mathf.RoundToInt(realworldDistanceOfChunk * percentageOfChunk);
        distance.text = finishDistance * realworldDistanceOfChunk - currentTotalDistance + 50 + "m";
    }

    public void SetScrollEnvironment(ScrollEnvironment _scrollEnvironment)
    {
        scrollEnvironment = _scrollEnvironment;
    }

    public void SetFinishDistance(int _finishDistance)
    {
        finishDistance = _finishDistance;
    }
}
