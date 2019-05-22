using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour {



    GameManager gameManager;

    Rigidbody rb;
    public float speed;
    public float maxSpeed = 4.5f;

    public float healspeedMultiplier = 1.05f;
    public float shootspeedMultiplier = 1.03f;

    public float healscaleModifier = 1.1f;
    public float shootscaleModifier = 1.1f;

    public float minSize = 0.28f;
    public float maxSize = 0.8f;

    public GameObject projectile;
    public GameObject projectileParent;

    public AudioClip popsound;
    public AudioClip healsound;
    public AudioClip shootsound;
    public AudioClip errorsound;
    public AudioClip hurtsound;
    public AudioClip deathsound;

    public int enemiesKilled;
    public int highscore;

    AudioSource audioSource;

    Material playermat;

    public bool canBeHurt = true;

    public TextMeshProUGUI scoreText;
 

    public GameObject healsParent;

    public GameObject playerhurteffect;

  
    private void Start() {
        playermat = gameObject.GetComponent<Renderer>().material;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        highscore = PlayerPrefs.GetInt("highscore");
        Time.timeScale = 1f;
        playerhurteffect = GameObject.Find("PlayerHurtEffect");
    }

    void Update() {

        scoreText.text = enemiesKilled.ToString();

        if(enemiesKilled >= highscore)
        {
            highscore = enemiesKilled;
        }

        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    void FixedUpdate() {
        Move();
        RotatePlayerToMouse();
    }



    public void Shoot() {
        if (transform.localScale.x > minSize)
        {
            audioSource.PlayOneShot(shootsound);
           
                gameObject.transform.localScale /= shootscaleModifier;
                speed *= shootspeedMultiplier;
            
        GameObject projectileobject = Instantiate(projectile, gameObject.transform.GetChild(0).transform.position, transform.rotation, projectileParent.transform);
        projectileobject.transform.localScale = new Vector3(transform.localScale.x * projectileobject.transform.localScale.x, transform.localScale.y * projectileobject.transform.localScale.y, transform.localScale.z * projectileobject.transform.localScale.z) ;
        } else
        {
            
            audioSource.PlayOneShot(errorsound, 0.25f);
          
        }

       
    }

    public void Move() {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0f,  Input.GetAxis("Vertical")) * speed;
    }

    public void RotatePlayerToMouse() {
       
        

            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);


            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));
        
        }


    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void Heal() {
        if(gameObject.transform.localScale.x < maxSize)
        {
            speed /= shootspeedMultiplier;
            transform.localScale *= healscaleModifier;
            audioSource.PlayOneShot(healsound);

        }
    }

    public void KillEnemy() {
        audioSource.PlayOneShot(popsound);
    }

    public void Hurt() {
        if (transform.localScale.x > minSize && canBeHurt)
        {
            audioSource.PlayOneShot(hurtsound);
            gameObject.transform.localScale /= healscaleModifier;
            speed *= healspeedMultiplier;
            playerhurteffect.transform.position = transform.position;
            playerhurteffect.GetComponent<ParticleSystem>().Play();
        } else if(transform.localScale.x < minSize && canBeHurt)
        { 
        
            // Game Over
            gameManager.GameOver();
            audioSource.PlayOneShot(deathsound, 0.25f);
        }
    }

    public IEnumerator GodMode() {
        canBeHurt = false;
        Debug.Log("test1");
        gameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
        yield return new WaitForSeconds(1f);
        Debug.Log("test2");
        gameObject.GetComponent<Renderer>().material = playermat;
        canBeHurt = true;
    }

}