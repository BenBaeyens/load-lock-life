using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteShootingPowerup : MonoBehaviour {
    GameObject player;
    GameManager gameManager;

    private void Start() {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)
        {
            StartCoroutine(player.GetComponent<PlayerController>().InfiniteShooting());
            player.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
            Destroy(gameObject);
        }
    }
}
