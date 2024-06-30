using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballSpawner : MonoBehaviour
{
    public float spawnDelay = 5f;
    public GameObject baseballPrefab;  // The baseball prefab to spawn
    private GameObject currentBaseball;
    private bool waiting = false;

    void Start()
    {

        // Spawn the first baseball
        SpawnBaseball();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentBaseball == null & !waiting) //no baseball and not waiting
        {
            //5s 后instantiate
            waiting = true;
            StartCoroutine(SpawnBaseballAfterDelay(spawnDelay));
        }
    }

    void SpawnBaseball()
    {
        currentBaseball = Instantiate(baseballPrefab, transform.position, transform.rotation);
    }

    private IEnumerator SpawnBaseballAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBaseball();
        waiting = false;
    }
}
