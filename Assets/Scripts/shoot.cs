using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    public AudioClip explosion;

    public GenerateObstacles generateObstacles;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null)
        {
            if (col.gameObject.transform.name == "ObstaclePrefab(Clone)" || col.gameObject.transform.name == "comet(Clone)")
            {
                print("yes");
                AudioSource.PlayClipAtPoint(explosion, col.transform.position);

                if (generateObstacles != null)
                {

                    generateObstacles.obstacles.Remove(col.gameObject);
                }
                Destroy(col.gameObject);
                Destroy(gameObject);
            }


        }
    }

    void OnEnable() {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Collect");

        foreach (GameObject obj in otherObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
}
}
