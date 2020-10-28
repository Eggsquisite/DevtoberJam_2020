using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public Text tempText;

    private int temperature = 1;    // 0 - low, 1 - med, 2 - high

    // Start is called before the first frame update
    void Start()
    {
        CheckTemp();
    }

    public void CheckTemp()
    {
        if (temperature == 0)
            tempText.text = "Low";
        else if (temperature == 1)
            tempText.text = "Med";
        else if (temperature == 2)
            tempText.text = "High";
    }

    public void IncreaseTemp()
    {
        if (temperature < 2)
            temperature++;

        CheckTemp();
    }

    public void DecreaseTemp()
    {
        if (temperature > 0)
            temperature--;

        CheckTemp();
    }

    public int GetTemp()
    {
        return temperature;
    }
}
