using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkTarget : MonoBehaviour
{
    [SerializeField]
    public GameObject objTarget;
    public float jarak;

    Vector3 newTarget;
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        //agent.destination = this.target.position;
        objTarget = GameObject.Find("Cylinder");

        //agent.enabled = false;
        agent.speed = 0f;
        newTarget = new Vector3(
            objTarget.transform.position.x,
            objTarget.transform.position.y,
            objTarget.transform.position.z
            );

        agent.destination = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        newTarget = new Vector3(
            objTarget.transform.position.x,
            objTarget.transform.position.y,
            objTarget.transform.position.z
            );

        agent.destination = newTarget;
        jarak = Vector3.Distance(this.transform.position, objTarget.transform.position);
        if (jarak < 10f)
        {
            agent.speed = 3.5f;
        }
        if (jarak < 2f) { Destroy(this.gameObject); }
    }
}
