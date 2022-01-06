using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AiScript : MonoBehaviour
{
    [Header("Book")]
    [SerializeField] private GameObject bookObj;
    [SerializeField] private float distanceToBook, delayDestory;
    [SerializeField] private int jarakMax;
    [SerializeField] private bool isBookReady;

    [Header("Game Object AI")]
    [SerializeField] private Animator aiAnim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speedAi;

    [SerializeField] private Transform startPos;

    private void Start()
    {
        SetAllComponenet();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void CheckTarget()
    {
        bookObj = GameObject.FindGameObjectWithTag("Book");
        if(bookObj != null)
        {
            distanceToBook = Vector3.Distance(this.gameObject.transform.position, bookObj.transform.position);
            isBookReady = true;
            if(distanceToBook < jarakMax)
            {
                agent.speed = speedAi;
                agent.isStopped = false;
                agent.autoRepath = true;
                aiAnim.SetBool("Moving", true);
                agent.SetDestination(bookObj.transform.position);

                if (agent.remainingDistance < 0.6f && isBookReady == true)
                {
                    StartCoroutine(DelayDestroyBook(delayDestory));
                    agent.SetDestination(startPos.transform.position);
                    agent.speed = speedAi -2.5f;
                    agent.autoRepath = true;
                }
            }
            else
            {
                agent.SetDestination(startPos.transform.position);
                agent.speed = speedAi - 2.5f;
                agent.autoRepath = true;
                if(agent.remainingDistance < 0.6f)
                {
                    agent.isStopped = true;
                    aiAnim.SetBool("Moving", false);
                }
            }

        }
    }

    IEnumerator DelayDestroyBook(float waktu)
    {
        aiAnim.SetBool("Attack", true);
        yield return new WaitForSeconds(waktu);
        Destroy(bookObj);
        isBookReady = false;
        aiAnim.SetBool("Attack", false);
    }
    private void SetAllComponenet()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        aiAnim = this.gameObject.GetComponent<Animator>();
        isBookReady = false;
        startPos = this.gameObject.transform;
    }
}
