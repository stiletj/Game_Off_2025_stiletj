using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollEnvironment : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject environPrefab;
    public int distance;

    private Vector3 updatePos;
    private Vector3 offsetVec;
    private GameObject front;
    private GameObject back;

    private bool hasUpdated;
    private bool isPaused;
    private float defaultSpeed;

    // Start is called before the first frame update
    void Start()
    {
        updatePos = transform.position;
        offsetVec = new Vector3(0, 0, 40);
        hasUpdated = false;
        isPaused = false;

        front = Instantiate(environPrefab);
        front.transform.position = updatePos + new Vector3(0, 0, 1);

        back = Instantiate(environPrefab);
        back.transform.position = front.transform.position - offsetVec;

        defaultSpeed = scrollSpeed;

        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            UpdateEnvironmentPosition(scrollSpeed);
            UpdateEnvironmentLoading();
        }
    }

    private void UpdateEnvironmentLoading()
    {
        if (front.transform.position.z <= updatePos.z + 0.2 && front.transform.position.z >= updatePos.z - 0.2)
        {
            if (!hasUpdated)
            {
                Destroy(back);
                back = null;
                back = front;
                front = null;
                front = Instantiate(environPrefab);
                front.transform.position = back.transform.position + offsetVec;
                hasUpdated = true;

                distance++;
            }
        }
        else if (back.transform.position.z <= updatePos.z + 0.2 && back.transform.position.z >= updatePos.z - 0.2)
        {
            if (!hasUpdated)
            {
                Destroy(front);
                front = null;
                front = back;
                back = null;
                back = Instantiate(environPrefab);
                back.transform.position = front.transform.position - offsetVec;
                hasUpdated = true;

                distance--;
            }
        }
        else
        {
            hasUpdated = false;
        }
    }

    public void UpdateEnvironmentPosition(float scrollSpeed)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Scrollable"))
        {
            obj.transform.position += new Vector3(0, 0, scrollSpeed * Time.deltaTime);
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Play()
    {
        scrollSpeed = defaultSpeed;
        isPaused = false;
    }

    public void IncrementDefaultSpeed(float amount)
    {
        defaultSpeed += amount;
    }
}
