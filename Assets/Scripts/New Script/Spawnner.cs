using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject prefabsAI;
    [SerializeField] private GameObject containnerAI;
    [SerializeField] private Transform[] spawnPos;

    [Header("Interval")]
    private int countDemon = 0;
    [SerializeField] private int time;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            int r = Random.Range(0, spawnPos.Length);
            Instantiate(prefabsAI, spawnPos[r].position, Quaternion.identity, containnerAI.transform);
            countDemon += 1;
            if(containnerAI.transform.childCount < 1)
            {
                Debug.Log("Spawn Tidak bisa");
            }
        }
    }
}
