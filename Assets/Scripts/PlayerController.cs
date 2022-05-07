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

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col!=null)
        {
             AudioSource.PlayClipAtPoint(crashSound,col.transform.position);
        Destroy(col.gameObject);
        }
    }
   
}