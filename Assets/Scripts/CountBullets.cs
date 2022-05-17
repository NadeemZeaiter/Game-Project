using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBullets : MonoBehaviour
{
    // Start is called before the first frame update
    public Text BulletsText;
    
   
    // Update is called once per frame
    public void NumBullets(int num)
    {
        
        BulletsText.text= ": " + num.ToString("0");
    }
   
}
