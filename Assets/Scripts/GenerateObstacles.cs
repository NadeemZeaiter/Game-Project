using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public Sprite[] images;
    float height, width;
    Vector3[] positions = new Vector3 [3];

    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenWorld = new Vector3(Screen.width, Screen.height, 0);
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
       // print(places[Random.Range(0,places.Length)]);
        
    }


    IEnumerator spawn(){
        while (true){
        GameObject v = GameObject.Instantiate(obstacle);
        v.transform.localScale = new Vector3(0.1388645f,0.1726376f,1);
        v.transform.position = positions[Random.Range(0,positions.Length)];
        v.GetComponent<SpriteRenderer>().sprite = images[Random.Range(0, images.Length)];
        yield return new WaitForSeconds(Random.Range(2f, 4f));
    }
    }

}
