using TMPro;
using UnityEngine;

public class SetFinalScore : MonoBehaviour
{
    private TextMeshProUGUI time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = GetComponent<TextMeshProUGUI>();

        int score = PlayerPrefs.GetInt("LastScore");

        time.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
