using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] spawnPoints = new GameObject[2];
    public GameObject[] items = new GameObject[4];
    public bool[] percent = {false, true, false, false, true, false, false, true, false};

    void Start()
    {
        if (this.transform.CompareTag("MovingMap"))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject item = items[Random.Range(0, 4)];
                item = Instantiate(item, new Vector3(spawnPoints[i].transform.position.x, item.transform.position.y, spawnPoints[i].transform.position.z), Quaternion.Euler(0, 90, 0));
                item.transform.parent = this.transform;
            }
        } 
        else
        {
            if (percent[Random.Range(0, 9)])
            {
                GameObject spawnPoint = spawnPoints[Random.Range(0, 2)];
                GameObject item = items[Random.Range(0, 4)];
                item = Instantiate(item, new Vector3(spawnPoint.transform.position.x, item.transform.position.y, spawnPoint.transform.position.z), Quaternion.Euler(0, 90, 0));
                item.transform.parent = this.transform;
            }
        }
    }

    void Update()
    {
        
    }
}
