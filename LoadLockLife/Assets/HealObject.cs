using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealObject : MonoBehaviour
{
    NavMeshAgent agent;

    public float radius;
    Transform player;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Update() {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= radius && player.GetComponent<PlayerController>().transform.localScale.x <= player.GetComponent<PlayerController>().maxSize)
            agent.destination = player.position;
        else
            agent.destination = transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().gameObject.transform.localScale.x > other.gameObject.GetComponent<PlayerController>().maxSize)
            {
                other.gameObject.GetComponent<PlayerController>().Heal();
            } else
            {
                other.gameObject.GetComponent<PlayerController>().Heal();
                 Destroy(gameObject);
            }
        }
    }

   
}
