using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipHearts : MonoBehaviour
{
   public GameObject hearts;
    public float heart_t = 4.0f;
    private Vector2 screenBounds;
    public GameObject plane;
   

    private Bounds bounds;
    // Use this for initialization
    void Start () {
        bounds = plane.GetComponent<Renderer>().bounds;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(heartWave());
    }
   
    private void spawnHeart(){
        GameObject a = Instantiate(hearts) as GameObject;
        a.transform.position = new Vector3(bounds.extents.x -2, Random.Range(-bounds.extents.y, bounds.extents.y),-1);
    }
    IEnumerator heartWave(){
        while(true){
            yield return new WaitForSeconds(heart_t);
            spawnHeart();
        }
    }
}
