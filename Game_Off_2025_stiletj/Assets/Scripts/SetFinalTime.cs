using TMPro;
using UnityEngine;

public class SetFinalTime : MonoBehaviour
{
    private TextMeshProUGUI time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = GetComponent<TextMeshProUGUI>();

        int min = PlayerPrefs.GetInt("LastMin");
        int sec = PlayerPrefs.GetInt("LastSec");
        float ms = PlayerPrefs.GetFloat("LastMs");

        time.text = "Time: " + min.ToString() + ":" + sec.ToString() + ":" + Mathf.RoundToInt(ms).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
