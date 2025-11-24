using TMPro;
using UnityEngine;

public class EndResults : MonoBehaviour
{
    public TextMeshProUGUI distance;
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;

    public void UpdateResults(int _distance, int min, int sec, int ms, int _score)
    {
        distance.text = "Distance: " + _distance + "m";
        time.text = "Time: " + min + ":" + sec + ":" + ms;
        score.text = "Score: " + _score;
    }
}
