using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipBullets : MonoBehaviour
{
   public GameObject bullets;
    public float bullet_t = 3.5f;
    private Vector2 screenBounds;
    public GameObject plane;
   

    private Bounds bounds;
    // Use this for initialization
    void Start () {
        bounds = plane.GetComponent<Renderer>().bounds;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }
    private void spawnEnemy(){
        GameObject a = Instantiate(bullets) as GameObject;
        a.transform.position = new Vector3(bounds.extents.x -2, Random.Range(-bounds.extents.y, bounds.extents.y),-1);
    }
    IEnumerator asteroidWave(){
        while(true){
            yield return new WaitForSeconds(bullet_t);
            spawnEnemy();
        }
    }
}
