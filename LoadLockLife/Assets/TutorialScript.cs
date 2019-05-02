using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public int tutorialChapter;

    public GameObject[] enemies;

    private void Update() {

        

        if (enemies[0] == null && tutorialChapter == 0) 
        {
            tutorialChapter++;
            Chapter1Start();

        }
    }

    void Chapter1Start() {
        Debug.Log("Start next chapter");
    }

}
