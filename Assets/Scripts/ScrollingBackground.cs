using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{

    public Text speedInxcreasedText;

    int timeToDisplayText = 1;

    float timer = 0.0f;

    bool enabled = false;
    Renderer rend;

    float accumulate = 0.0f;

    int speedIndex = -1;

    float[] scrollSpeeds = {0.5f,1f,1.5f,2f,2.5f,3f,3.5f,4f};

    public float scrollSpeed = 0.5f;
    public bool xAxis;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(incrementSpeed());
        speedInxcreasedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        float offset = Time.deltaTime * scrollSpeed;
        accumulate+=offset;
        if (!xAxis)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, accumulate));
        } else
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(accumulate,0));
        }

        if(enabled)
        {
            timer+=Time.deltaTime;
            if((int)timer == timeToDisplayText)
            {
                timer = 0.0f;
                enabled = false;
                speedInxcreasedText.enabled = false;
            }
        }
    }

    
    IEnumerator incrementSpeed(){
        while (true){
            speedIndex+=1;
            if(speedIndex != 8)
            {
                scrollSpeed = scrollSpeeds[speedIndex];
                enabled = true;
                speedInxcreasedText.enabled = true;
            }
        
        yield return new WaitForSeconds(20f);
    }
    }
}