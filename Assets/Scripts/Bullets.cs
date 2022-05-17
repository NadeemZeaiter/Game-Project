using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets: MonoBehaviour
{
    public GameObject bullets;
    float height, width;
    Vector3[] positions = new Vector3 [3];

    Vector3 screenWorld;
    public List<GameObject> obstacles = new List<GameObject>();
   
    // Start is called before the first frame update
    void Start()
    {
        screenWorld = new Vector3(Screen.width, Screen.height, 0);
        screenWorld = Camera.main.ScreenToWorldPoint(screenWorld);
        height = screenWorld.y;
        width = screenWorld.x;
        StartCoroutine(spawn());
        positions[0] = new Vector3(-1.72f,4.3f,-1);
        positions[1]= new Vector3(0.0104f,4.3f,-1);
        positions[2] = new Vector3(1.67f,4.3f,-1);
    }

    // Update is called once per frame
    void Update()
    {
        checkToDestroy();
        
    }
    


    IEnumerator spawn(){
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        while (true){
        GameObject v = GameObject.Instantiate(bullets);
        v.transform.localScale = new Vector3(0.1388645f,0.1726376f,1);
        v.transform.position = positions[Random.Range(0,positions.Length)];
        obstacles.Add(v);
        yield return new WaitForSeconds(Random.Range(2f, 4f));
    }
    }


    void checkToDestroy()
    {
        foreach(var obs in obstacles)
        {
            if(obs!=null)
            {
            if(obs.transform.position.y<-5)
            {
                obstacles.Remove(obs);
                Destroy(obs);
            }
            }
        }
    }
    void OnBecameInvisible() {
         Destroy(gameObject);
     }
     void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Shot");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
