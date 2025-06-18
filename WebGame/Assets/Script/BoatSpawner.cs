using UnityEngine;
using System.Collections.Generic;

public class BoatSpawner : MonoBehaviour
{
    public GameObject boatPrefab;
    public Transform player;
    public float boatSpacing = 13f;
    public int maxBoats = 20;

    public float spawnPosition = 50f;     // Z position where boats spawn
    public float DestroyPosition = 0f;    // Z position where boats are destroyed

    public float Spawninterval = 2f;      // Time between each spawn

    private float timer = 0f;
    private List<GameObject> activeBoats = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        // Spawn a boat at intervals
        if (timer >= Spawninterval && activeBoats.Count < maxBoats)
        {
            timer = 0f;
            SpawnBoat();
        }

        // Destroy boats that passed the destroy position
        for (int i = activeBoats.Count - 1; i >= 0; i--)
        {
            if (activeBoats[i].transform.position.z <= DestroyPosition)
            {
                Destroy(activeBoats[i]);
                activeBoats.RemoveAt(i);
            }
        }
    }

    void SpawnBoat()
    {
        float randomX = Random.Range(-4f, 4f);
        Vector3 spawnPos = new Vector3(randomX, 0f, spawnPosition);
        GameObject newBoat = Instantiate(boatPrefab, spawnPos, Quaternion.identity);
        activeBoats.Add(newBoat);
    }
}
