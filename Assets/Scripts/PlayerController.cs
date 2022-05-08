using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 100f;

    public GenerateObstacles generateObstacles;

    public GameObject fire;

    SpriteRenderer renderer;

    public AudioClip crashSound;


    float nextFire = 0.5f;
    float firingTimer = 0.0f;

    float accumulate = 0.0f;
    private float direction;

    public GameObject road;

    Vector2 roadBounds;
  
    // Start is called before the first frame update
    void Start()
    {
         print("before renderer");
        renderer = transform.GetComponent<SpriteRenderer>();

        print("rendererrr "+renderer);
        roadBounds = road.transform.GetComponent<SpriteRenderer>().bounds.extents;

    }

    // Update is called once per frame
    void Update()
    {
       direction = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
       accumulate+=direction;

       transform.position = new Vector3(accumulate,transform.position.y,-1);
       
       Vector3 viewPos = transform.position;
       viewPos.x = Mathf.Clamp(viewPos.x,roadBounds.x-2,roadBounds.x+2);
        transform.position = viewPos;

       if (Input.GetMouseButtonDown(0) && firingTimer>=nextFire)
        {
            firingTimer=0.0f;
            print("mouse pressed !! ");
            GameObject shot = GameObject.Instantiate(fire);
            shot.transform.position = new Vector3(transform.position.x,transform.position.y+2f,transform.position.z);

            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0,5f);
        }

        firingTimer+=Time.deltaTime;

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col!=null)
        {
            if(col.gameObject.transform.name == "ObstaclePrefab(Clone)")
            {
              StartCoroutine(DoBlinks(3,0.2f));
             AudioSource.PlayClipAtPoint(crashSound,col.transform.position);
             generateObstacles.obstacles.Remove(col.gameObject);
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