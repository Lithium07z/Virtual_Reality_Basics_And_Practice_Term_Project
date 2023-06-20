using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform playerPosition;
    public GameObject[] maps = new GameObject[5];
    private int numSpawnedMap;

    public GameObject[] arr = new GameObject[5];
    private int idx = 0;

    void Start()
    {
        numSpawnedMap = 5;
    }

    void Update()
    {
        if (playerPosition.position.z >= 60 * (numSpawnedMap - 4))
        {
            Vector3 nextSpawn = new Vector3(0, 0, 60 * numSpawnedMap);
            GameObject map = Instantiate(maps[Random.Range(0, 5)], nextSpawn, this.transform.rotation);
            Destroy(arr[idx % 5]);
            arr[idx % 5] = map;
            idx++;
            numSpawnedMap++;
            GameObject.Find("Player").GetComponent<PlayerCtrl>().countMap++;
        }
    }
}
