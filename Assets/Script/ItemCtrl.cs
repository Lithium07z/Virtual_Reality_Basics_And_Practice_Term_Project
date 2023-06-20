using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public GameObject particle;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        this.transform.RotateAround(this.transform.position, Vector3.down, Time.deltaTime * 70); // 아이템 회전
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Instantiate(particle, this.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
        }    
    }
}
