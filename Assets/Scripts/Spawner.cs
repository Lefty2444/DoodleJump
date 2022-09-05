using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawnable
{
    public GameObject prefab;
    // A decimal representing the percent chance of spawn
    public float frequency;
}

public class Spawner : MonoBehaviour
{
    // All spawnable frequencies should add up to one (100%)
    public Spawnable[] spawnables;

    public Vector2 spawningXPosRange = new Vector2(-8, 8);
    public Vector2 spawningThresholdRange = new Vector2(1,3);

    
    void Start()
    {
        StartCoroutine(Spawning());   
    }

    IEnumerator Spawning()
    {
        float yPos = transform.position.y;
        while (true)
        {
            float spawningThreshold = Random.Range(spawningThresholdRange.x, spawningThresholdRange.y);
            while (transform.position.y - yPos < spawningThreshold)
                yield return null;
            Vector2 spawnPosition = transform.position;
            spawnPosition.x = Random.Range(spawningXPosRange.x, spawningXPosRange.y);
            Instantiate(GetSpawnable(), spawnPosition, Quaternion.identity);
            yPos = transform.position.y;
        }
    }

    public GameObject GetSpawnable()
    {
        float randomNum = Random.Range(0, 1f);
        foreach (Spawnable spawnable in spawnables)
        {
            if (randomNum < spawnable.frequency)
            {
                return spawnable.prefab;
            }
            randomNum -= spawnable.frequency;
        }
        return spawnables[0].prefab;
    }
}
