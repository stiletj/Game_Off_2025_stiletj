using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollEnvironment : MonoBehaviour
{
    public Vector3 startPos;
    public float scrollSpeed;
    public GameObject environPrefab;
    private List<Object> scrollableEnvironment = new List<Object>();
    private GameObject backTrigger;
    private GameObject frontTrigger;
    private int currentEnvironment;

    // Start is called before the first frame update
    void Start()
    {
        currentEnvironment = 0;
        scrollableEnvironment.Add(Instantiate(environPrefab));
        //set front trigger
        //set back trigger
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);

        if (other.gameObject == backTrigger)
        {
            //Set spawnPos to behind current environment
        }
        else if (other.gameObject == frontTrigger)
        {
            //Set spawnPos to in front of current environment
        }

        //Instantiate environPrefab and add to scrollableEnvironment list
    }
}
