using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBackground : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material mat;
    public Text speedInxcreasedText;
    bool enabled = false;
    int speedIndex = -1;
    float accumulate = 0.0f;
    float timer = 0.0f;
    int timeToDisplayText = 1;

    float[] scrollSpeeds = { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        StartCoroutine(incrementSpeed());
        speedInxcreasedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        accumulate+=offset;
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


    IEnumerator incrementSpeed()
    {
        while (true)
        {
            speedIndex += 1;
            if (speedIndex != 8)
            {
                scrollSpeed = scrollSpeeds[speedIndex];
                enabled = true;
                speedInxcreasedText.enabled = true;
            }

            yield return new WaitForSeconds(20f);
        }
    }
}
