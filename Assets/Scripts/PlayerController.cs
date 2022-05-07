using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 100f;

    public GenerateObstacles generateObstacles;

    public AudioClip crashSound;

    float accumulate = 0.0f;
    private float direction;

    public GameObject road;

    Vector2 roadBounds;
  
    // Start is called before the first frame update
    void Start()
    {
        roadBounds = road.transform.position;
        print("road bounds: "+roadBounds);
    }

    // Update is called once per frame
    void Update()
    {
       direction = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
       print("Input.GetAxis "+Input.GetAxis("Horizontal"));
       print("moveSpeed "+moveSpeed);
       print("delta time "+Time.deltaTime);

       accumulate+=direction;

       transform.position = new Vector3(accumulate,transform.position.y,-1);
       
       Vector3 viewPos = transform.position;
       viewPos.x = Mathf.Clamp(viewPos.x,-5,5);
        transform.position = viewPos;

    }


    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(col!=null)
    //     {
    //          AudioSource.PlayClipAtPoint(crashSound,col.transform.position);
    //          print("collision "+col.transform.name);
    //     }
    // }
   
}