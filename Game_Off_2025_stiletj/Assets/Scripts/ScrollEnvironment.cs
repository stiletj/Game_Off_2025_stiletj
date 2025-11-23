using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollEnvironment : MonoBehaviour
{
    public GameObject roadPrefab;
    public List<GameObject> environPrefabs;
    public GameObject finalChunkPrefab;

    public float scrollSpeed;
    public int distance;
    public GameObject front;
    public GameObject back;

    private GameObject frontEnviron;
    private GameObject backEnviron;

    private Vector3 updatePos;
    private Vector3 offsetVec;
    private Vector3 finalChunkOffset;

    private bool hasUpdated;
    private bool isPaused;
    private float defaultSpeed;
    private bool isEnding;
    private int finishDistance;

    // Start is called before the first frame update
    void Start()
    {
        updatePos = transform.position;
        offsetVec = new Vector3(0, 0, 40);
        finalChunkOffset = new Vector3(1.4f, 0, 51.39349f);
        hasUpdated = false;
        isPaused = false;
        isEnding = false;

        front = Instantiate(roadPrefab);
        front.transform.position = updatePos + new Vector3(0, 0, 1);
        frontEnviron = Instantiate(environPrefabs[GetRandomEnviron()]);
        frontEnviron.transform.position = front.transform.position;

        back = Instantiate(roadPrefab);
        back.transform.position = front.transform.position - offsetVec;
        backEnviron = Instantiate(environPrefabs[GetRandomEnviron()]);
        backEnviron.transform.position = back.transform.position;

        defaultSpeed = scrollSpeed;
        distance = 0;
        finishDistance = GameObject.Find("GameManager").GetComponent<GameManager>().finishDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance == finishDistance)
        {
            isEnding = true;
        }

        //Debug.Log(isPaused);

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
                Destroy(backEnviron);

                back = null;
                backEnviron = null;

                back = front;
                backEnviron = frontEnviron;

                front = null;
                frontEnviron = null;

                if (!isEnding)
                {
                    front = Instantiate(roadPrefab);
                    front.transform.position = back.transform.position + offsetVec;
                    frontEnviron = Instantiate(environPrefabs[GetRandomEnviron()]);
                    frontEnviron.transform.position = front.transform.position;

                    hasUpdated = true;
                    distance++;
                }
                else
                {
                    LoadFinalChunk();
                }
            }
        }
        else if (back.transform.position.z <= updatePos.z + 0.2 && back.transform.position.z >= updatePos.z - 0.2)
        {
            if (!hasUpdated)
            {
                Destroy(front);
                Destroy(frontEnviron);

                front = null;
                frontEnviron = null;

                front = back;
                frontEnviron = backEnviron;

                back = null;
                backEnviron = null;

                back = Instantiate(roadPrefab);
                back.transform.position = front.transform.position - offsetVec;
                backEnviron = Instantiate(environPrefabs[GetRandomEnviron()]);
                backEnviron.transform.position = back.transform.position;

                hasUpdated = true;
                distance--;
            }
        }
        else
        {
            hasUpdated = false;
        }
    }

    private void LoadFinalChunk()
    {
        front = Instantiate(finalChunkPrefab);
        front.transform.position = back.transform.position + finalChunkOffset;

        hasUpdated = true;

        NPCSpawner spawner = GetComponent<NPCSpawner>();

        spawner.PauseSpawning();
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

    public bool IsPaused()
    {
        return isPaused;
    }

    public void IncrementDefaultSpeed(float amount)
    {
        defaultSpeed += amount;
    }

    private int GetRandomEnviron()
    {
        return Random.Range(0, environPrefabs.Count);
    }
}
