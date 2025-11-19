using TMPro;
using UnityEngine;

public class EndResults : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;

    public void UpdateResults(int min, int sec, int ms, int _score)
    {
        time.text = "Time: " + min + ":" + sec + ":" + ms;
        score.text = "Score: " + _score;
    }
}
