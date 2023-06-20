using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public GameObject[] spawnPoints = new GameObject[4];
    public GameObject[] cars = new GameObject[10];
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(spawn());
    }

    void Update()
    {
        
    }

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f); // 1.2초마다 스폰

            if (player.transform.position.z - this.transform.position.z >= 4) // 플레이어가 스폰과 가까워지면
            {
                break; // 스폰 중지
            }

            GameObject spawnPoint = spawnPoints[Random.Range(0, 4)];
            GameObject car = cars[Random.Range(0, 10)];
            car = Instantiate(car, new Vector3(spawnPoint.transform.position.x, car.transform.position.y, spawnPoint.transform.position.z), Quaternion.Euler(0, 90, 0));
            car.AddComponent<CarMoving>();
        }
    }
}
