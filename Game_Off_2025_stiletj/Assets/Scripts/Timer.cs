using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;

    private int currentMin;
    private int currentSec;
    private float currentMs;

    private bool isPlaying;
    private float roundedNum;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer.text = "0:0:0";
        currentMin = 0;
        currentSec = 0;
        currentMs = 0;
        isPlaying = false;
        roundedNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            currentMs += Time.deltaTime;

            if (currentMs >= 1)
            {
                currentSec++;
                currentMs = 0;

                if (currentSec == 60)
                {
                    currentMin++;
                    currentSec = 0;
                }
            }

            roundedNum = Mathf.Round((currentMs * 100f) * 1f) * 1f;
            if (roundedNum < 10)
            {
                timer.text = currentMin.ToString() + ":" + currentSec.ToString() + ":0" + roundedNum.ToString();
            }
            else
            {
                timer.text = currentMin.ToString() + ":" + currentSec.ToString() + ":" + roundedNum.ToString();
            }
        }
    }

    public void StartTimer()
    {
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }

    public void ResetTimer()
    {
        isPlaying = false;
        currentMin = 0;
        currentSec = 0;
        currentMs = 0;

        timer.text = "0:0:0";
    }
}
