using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public GameObject obstacle;

    public GameObject enemy;


    public Sprite[] images;
    float height, width;
    Vector3[] positions = new Vector3 [3];

    Vector3 screenWorld;
    public List<GameObject> obstacles = new List<GameObject>();

    private IEnumerator coroutine;


    
    // Start is called before the first frame update
    void Start()
    {
        screenWorld = new Vector3(Screen.width, Screen.height, 0);
        screenWorld = Camera.main.ScreenToWorldPoint(screenWorld);
        height = screenWorld.y;
        width = screenWorld.x;
        coroutine = spawn();
        StartCoroutine(coroutine);
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
        GameObject v = GameObject.Instantiate(obstacle);
        v.transform.localScale = new Vector3(0.1388645f,0.1726376f,1);
        v.transform.position = positions[Random.Range(0,positions.Length)];
        v.GetComponent<SpriteRenderer>().sprite = images[Random.Range(0, images.Length)];
        obstacles.Add(v);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
    }
    }

    public void spawnEnemy()
    {
        GameObject v = GameObject.Instantiate(enemy);
        v.transform.localScale = new Vector3(1f,1f,1f);
        v.transform.position = positions[Random.Range(0,positions.Length)];
        v.transform.position = new Vector3(v.transform.position.x,3.8f,v.transform.position.z);
         GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);

    }


    public void stopSpawning()
    {
        StopCoroutine(coroutine);
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

}
