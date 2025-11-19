using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public float waitTime = 2;

    private ScrollEnvironment scrollEnvironment;
    private float timer;
    private bool start;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scrollEnvironment = GameObject.Find("Environment Manager").GetComponent<ScrollEnvironment>();
        timer = waitTime;
        start = false;
    }

    private void Update()
    {
        if (start)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameObject.Find("SceneManager").GetComponent<MenuButtonEvents>().EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scrollEnvironment.Pause();
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowEndResults();
            start = true;
        }
    }
}
