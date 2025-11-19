using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public GameObject canvas;
    public Vector3 minSpawnRange;
    public Vector3 maxSpawnRange;
    public Vector3 minDespawnRange;
    public Vector3 maxDespawnRange;
    public List<Vector3> minSpawnRanges;
    public List<Vector3> maxSpawnRanges;
    public List<Vector3> minDespawnRanges;
    public List<Vector3> maxDespawnRanges;

    private List<GameObject> npcList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Spawn(GenerateRandomLocation());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            for (int j = 0; j < maxDespawnRanges.Count; j++)
            {
                if (npcList[i].transform.position.x <= maxDespawnRanges[j].x && npcList[i].transform.position.x >= minDespawnRanges[j].x)
                {
                    if (npcList[i].transform.position.y <= maxDespawnRanges[j].y && npcList[i].transform.position.y >= minDespawnRanges[j].y)
                    {
                        if (npcList[i].transform.position.z <= maxDespawnRanges[j].z && npcList[i].transform.position.z >= minDespawnRanges[j].z)
                        {
                            Despawn(i);
                        }
                    }
                }
            }
        }
    }

    private List<Vector3> GenerateRandomLocation(bool spawnAllLocations)
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 pos = Vector3.zero;

        if (spawnAllLocations)
        {
            for (int i = 0; i < minSpawnRanges.Count; i++)
            {
                pos.x = Random.Range(minSpawnRanges[i].x, maxSpawnRanges[i].x);
                pos.y = Random.Range(minSpawnRanges[i].y, maxSpawnRanges[i].y);
                pos.z = Random.Range(minSpawnRanges[i].z, maxSpawnRanges[i].z);

                list.Add(pos);
            }
        }
        else
        {
            int i = Random.Range(0, minSpawnRanges.Count);

            pos.x = Random.Range(minSpawnRanges[i].x, maxSpawnRanges[i].x);
            pos.y = Random.Range(minSpawnRanges[i].y, maxSpawnRanges[i].y);
            pos.z = Random.Range(minSpawnRanges[i].z, maxSpawnRanges[i].z);

            list.Add(pos);
        }

        return list;
    }

    private void Spawn()
    {
        List<Vector3> location = GenerateRandomLocation(false);

        for (int i = 0; i < location.Count; i++)
        {
            npcList.Add(Instantiate(npcPrefab));
            npcList[npcList.Count - 1].transform.position = location[i];
            npcList[npcList.Count - 1].GetComponent<NPCInteraction>().canvas = canvas;
        }
    }

    private void Despawn(int i)
    {
        Destroy(npcList[i]);
        npcList.RemoveAt(i);
    }

    public void SpawnNPC(int min, int sec)
    {
        if (sec*0 == 0)
        {
            Spawn();
        }
    }
}
