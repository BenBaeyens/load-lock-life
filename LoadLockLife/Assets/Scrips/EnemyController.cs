using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject deathEffect;
    Transform player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < temp.Length; i++)
        {
            if(temp[i].name == "DeathEffect")
            {
                deathEffect = temp[i];
                break;
            }
        }
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
            agent.SetDestination(player.position);

            agent.stoppingDistance = 0;


            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        
    }

    void FaceTarget() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {

            player.GetComponent<PlayerController>().Hurt();
            if(deathEffect != null)
               Instantiate(deathEffect, transform);
            Destroy(gameObject);
        }
    }
}
