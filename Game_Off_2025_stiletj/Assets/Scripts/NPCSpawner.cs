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

    private List<GameObject> npcList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNPC(GenerateRandomLocation());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            if (npcList[i].transform.position.x <= maxDespawnRange.x && npcList[i].transform.position.x >= minDespawnRange.x)
            {
                if (npcList[i].transform.position.y <= maxDespawnRange.y && npcList[i].transform.position.y >= minDespawnRange.y)
                {
                    if (npcList[i].transform.position.z <= maxDespawnRange.z && npcList[i].transform.position.z >= minDespawnRange.z)
                    {
                        DespawnNPC(i);
                        SpawnNPC(GenerateRandomLocation());
                    }
                }
            }
        }
    }

    private Vector3 GenerateRandomLocation()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minSpawnRange.x, maxSpawnRange.x);
        pos.y = Random.Range(minSpawnRange.y, maxSpawnRange.y);
        pos.z = Random.Range(minSpawnRange.z, maxSpawnRange.z);

        return pos;
    }

    private void SpawnNPC(Vector3 location)
    {
        npcList.Add(Instantiate(npcPrefab));
        npcList[npcList.Count - 1].transform.position = location;
        npcList[npcList.Count - 1].GetComponent<NPCInteraction>().canvas = canvas;
    }

    private void DespawnNPC(int i)
    {
        Destroy(npcList[i]);
        npcList.RemoveAt(i);
    }
}
