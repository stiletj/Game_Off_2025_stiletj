using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;

public class CarSpawner : MonoBehaviour
{
    public float minTime = 5;
    public float maxTime = 10;
    public float carSpeed;
    public float bikeSpeed;
    public List<GameObject> carPrefabs;

    public List<Transform> spawnPoints;
    public Transform bikeSpawnPoint;

    private float timer;
    private bool isPaused;
    private List<GameObject> cars = new List<GameObject>();
    private List<GameObject> pausedCars = new List<GameObject>();

    private bool movingIsPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SpawnCar();
                timer = Random.Range(minTime, maxTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!movingIsPaused)
        {
            MoveCars();
        }
    }

    private void SpawnCar()
    {
        int carNum = Random.Range(0, carPrefabs.Count);

        if (carPrefabs[carNum] != null)
        {
            cars.Add(Instantiate(carPrefabs[carNum]));
            if (carNum == carPrefabs.Count - 1)
            {
                cars[cars.Count - 1].transform.position = bikeSpawnPoint.position;
            }
            else
            {
                cars[cars.Count - 1].transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < cars.Count;i++)
        {
            if (cars[i] == other.gameObject)
            {
                Destroy(cars[i]);
                cars.RemoveAt(i);
            }
        }
    }

    private void MoveCars()
    {
        bool paused = false;

        for (int i = 0; i < cars.Count; i++)
        {
            paused = false;

            for (int j = 0; j < pausedCars.Count; j++)
            {
                if (pausedCars[j] == cars[i])
                {
                    paused = true;
                }
            }

            if (!paused)
            {
                if (cars[i] == carPrefabs[carPrefabs.Count - 1])
                {
                    cars[i].transform.position += new Vector3(0, 0, -bikeSpeed * Time.deltaTime);
                }
                else
                {
                    cars[i].transform.position += new Vector3(0, 0, -carSpeed * Time.deltaTime);
                }
            }
        }
    }

    public void PauseSpawning()
    {
        isPaused = true;
    }

    public void PlaySpawning()
    {
        isPaused = false;
    }

    public void PauseCar(GameObject car)
    {
        for (int i = 0; i < cars.Count;i++)
        {
            if (car == cars[i])
            {
                if (!pausedCars.Contains(cars[i]))
                {
                    pausedCars.Add(cars[i]);
                }
            }
        }
    }

    public void PlayCar(GameObject car)
    {
        for (int i = 0; i < cars.Count; i++)
        {
            if (car == cars[i])
            {
                pausedCars.Remove(cars[i]);
            }
        }
    }

    public void PauseAllMoving()
    {
        movingIsPaused = true;
    }

    public void PlayAllMoving()
    {
        movingIsPaused = false;
    }
}
