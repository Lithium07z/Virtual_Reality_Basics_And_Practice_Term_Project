using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    private int distance;

    void Start()
    {
        distance = (int)this.transform.position.z; // 최초 위치 저장
    }

    void Update()
    {
        if (distance - this.transform.position.z >= 40) // 처음 위치에서부터 40이상 벗어나면 
        {
            Destroy(this.gameObject); // 삭제
        }
        this.transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * 7); // 계속 도로따라서 등속이동
    }
}
