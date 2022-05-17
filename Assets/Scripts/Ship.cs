using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth=100;
    public healthBar healthbar;
     public GameObject gameover;
    public GameObject fire;
     float nextFire = 0.5f;
    float firingTimer = 0.0f;
    public AudioClip crashSound;
    SpriteRenderer renderer;
    public GenerateObstacles generateObstacles;
    public Score score;
     public GameObject plane;
    private Bounds bounds;
    public int bulletsNum=0;
    public CountBullets count_Bullets;
    // Start is called before the first frame update
    void Start()
    {
        gameover.SetActive(false); 
        renderer = transform.GetComponent<SpriteRenderer>();
        bounds = plane.GetComponent<Renderer>().bounds;

    }

    // Update is called once per frame
    void Update()
    {
        count_Bullets.NumBullets(bulletsNum);
        float amntToMove1 = Input.GetAxisRaw ("Vertical") * 5f * Time.deltaTime;
        transform.Translate(-Vector3.right *amntToMove1);

        Vector3 viewPos = transform.position;
        viewPos.y = Mathf.Clamp(viewPos.y,-bounds.extents.y+1,bounds.extents.y-1);
        transform.position = viewPos;

         if (Input.GetMouseButtonDown(0) && firingTimer>=nextFire)
        {
            if(bulletsNum>0){
                bulletsNum-=1;
                firingTimer=0.0f;
                print("mouse pressed !! ");
                GameObject shot = GameObject.Instantiate(fire);
                shot.transform.position = new Vector3(transform.position.x+2f,transform.position.y,transform.position.z);
                Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(5f,0);
        }
        }
        if(currentHealth!=0){
            score.updateScores();
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
             if(currentHealth>0){
                damage(20);
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
        
            if(currentHealth==0){
                Time.timeScale=0f;//freeze the game 
                gameover.SetActive(true);//place the gameover UI on the screen
            }
            if(col.gameObject.transform.name == "bullets(Clone)"){
                bulletsNum+=1;
                Destroy(col.gameObject);
            }
        
        
    }}
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
