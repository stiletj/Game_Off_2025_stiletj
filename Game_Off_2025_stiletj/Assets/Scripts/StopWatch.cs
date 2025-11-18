using TMPro;
using UnityEngine;

public delegate void OnTickFunc(int min, int sec);

public class StopWatch : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public int currentMin;
    public int currentSec;
    public float currentMs;

    private bool isPlaying;
    private float roundedNum;

    private OnTickFunc onSecondTick = new OnTickFunc(DefaultFunc);
    private OnTickFunc onMinuteTick = new OnTickFunc(DefaultFunc);

    void Start()
    {
        timer.text = "0:0:0";
        currentMin = 0;
        currentSec = 0;
        currentMs = 0;
        isPlaying = false;
        roundedNum = 0;

        StartTimer();
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
                if (onSecondTick != null)
                {
                    onSecondTick.Invoke(currentMin, currentSec);
                }

                if (currentSec == 60)
                {
                    currentMin++;
                    currentSec = 0;
                    if (onMinuteTick != null )
                    {
                        onMinuteTick.Invoke(currentMin, currentSec);
                    }
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

    public void SetOnSecondFunc(OnTickFunc func)
    {
        onSecondTick = func;
        onSecondTick.Invoke(currentMin, currentSec);
    }

    public void SetOnMinuteFunc(OnTickFunc func)
    {
        onMinuteTick = func;
    }

    public bool CheckOnSecondFunc()
    {
        if (onSecondTick != null)
        {
            return true;
        }

        return false;
    }

    public static void DefaultFunc(int min, int sec)
    {
        Debug.Log("Hello World");
    }
}
