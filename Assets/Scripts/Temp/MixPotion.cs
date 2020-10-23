using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixPotion : MonoBehaviour
{
    public float goalTemp, tempRange, tempSway, swayFreq, swayTimer;
    public Text tempText;

    private bool reset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempText.text = goalTemp.ToString();
    }

    void TempReset()
    {
        print("Button held down");
        reset = !reset;
    }
}
