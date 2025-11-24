using TMPro;
using UnityEngine;

public class SetFinalDistance : MonoBehaviour
{
    private TextMeshProUGUI distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = GetComponent<TextMeshProUGUI>();

        int dist = PlayerPrefs.GetInt("LastDistance");

        distance.text = "Distance: " + dist.ToString() + "m";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
