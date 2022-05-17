using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth=100;
    public healthBar healthbar;
    private Rigidbody2D rb;
    public float moveSpeed = 100f;
    public Score score;
    public CountBullets count_Bullets;

    public GenerateObstacles generateObstacles;

    public GameObject fire;
    public GameObject gameover;

    SpriteRenderer renderer;

    public AudioClip crashSound;
    public int bulletsNum=0;


    float nextFire = 0.5f;
    float firingTimer = 0.0f;

    float accumulate = 0.0f;
    private float direction;

    public GameObject road;

    Vector2 roadBounds;
  
    // Start is called before the first frame update
    void Start()
    {
        gameover.SetActive(false);   
        print("before renderer");
        renderer = transform.GetComponent<SpriteRenderer>();

        print("rendererrr "+renderer);
        roadBounds = road.transform.GetComponent<SpriteRenderer>().bounds.extents;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       count_Bullets.NumBullets(bulletsNum);
       direction = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
       print("horizontal move====="+Input.GetAxis("Horizontal"));
       accumulate+=direction;

       transform.position = new Vector3(accumulate,transform.position.y,-1);
       
       Vector3 viewPos = transform.position;
       viewPos.x = Mathf.Clamp(viewPos.x,roadBounds.x-2,roadBounds.x+2);
        transform.position = viewPos;

       if (Input.GetMouseButtonDown(0) && firingTimer>=nextFire)
        {
            if(bulletsNum>0){
                bulletsNum-=1;
                firingTimer=0.0f;
                print("mouse pressed !! ");
                GameObject shot = GameObject.Instantiate(fire);
                shot.transform.position = new Vector3(transform.position.x,transform.position.y+2f,transform.position.z);

                Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0,5f);
             }
            
        }

        firingTimer+=Time.deltaTime;

        if(currentHealth!=0){
            score.updateScores();
        }

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
             if(currentHealth>0){
                damage(20);
             }
             
             if(currentHealth==0){
                Time.timeScale=0f;//freeze the game 
                gameover.SetActive(true);//place the gameover UI on the screen
             }
            }
            if(col.gameObject.transform.name == "hearts(Clone)"){
                if(currentHealth<100){
                    Revive(20);
                    Destroy(col.gameObject);
                }
                else{
                    Destroy(col.gameObject);
                }
            
                
            }
            if(col.gameObject.transform.name == "bullets(Clone)"){
                bulletsNum+=1;
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
    void  damage(int damage_amt){
        currentHealth-=damage_amt;
        healthbar.setHealth(currentHealth);
    }
    void  Revive(int damage_amt){
        currentHealth+=damage_amt;
        healthbar.setHealth(currentHealth);
    }
   
}