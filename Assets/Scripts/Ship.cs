using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject fire;
     float nextFire = 0.5f;
    float firingTimer = 0.0f;
    public AudioClip crashSound;
    SpriteRenderer renderer;
    public GenerateObstacles generateObstacles;


    // Start is called before the first frame update
    void Start()
    {
        renderer = transform.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        float amntToMove1 = Input.GetAxisRaw ("Vertical") * 5f * Time.deltaTime;
        transform.Translate(-Vector3.right *amntToMove1);


         if (Input.GetMouseButtonDown(0) && firingTimer>=nextFire)
        {
            firingTimer=0.0f;
            print("mouse pressed !! ");
            GameObject shot = GameObject.Instantiate(fire);
            shot.transform.position = new Vector3(transform.position.x+2f,transform.position.y,transform.position.z);

            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(5f,0);
        }

        firingTimer+=Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col!=null)
        {
            if(col.gameObject.transform.name == "comet(Clone)" )
            {
              StartCoroutine(DoBlinks(3,0.2f));
             AudioSource.PlayClipAtPoint(crashSound,col.transform.position);
             if(generateObstacles != null)
             {

             generateObstacles.obstacles.Remove(col.gameObject);
             }
             Destroy(col.gameObject);
            }
        }
    }
    IEnumerator DoBlinks(int numBlinks, float seconds) {

        print("do blinksss");
     for (int i=0; i<numBlinks; i++) {

         //toggle renderer
         renderer.enabled = !renderer.enabled;
         
         //wait for a bit
         yield return new WaitForSeconds(seconds);
     }
     
     //make sure renderer is enabled when we exit
     renderer.enabled = true;
 }

}
