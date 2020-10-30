using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public RectTransform tempNeedle;
    public float speed;

    private bool decreaseTemp, increaseTemp;

    private int temperature = 1;    // 0 - low, 1 - med, 2 - high

    // Start is called before the first frame update
    void Start()
    {
        CheckTemp();
    }

    private void Update()
    {
        if (decreaseTemp)
            tempNeedle.rotation = Quaternion.RotateTowards(tempNeedle.rotation, Quaternion.Euler(0, 0, -30), Time.deltaTime * speed);
        else if (increaseTemp)
            tempNeedle.rotation = Quaternion.RotateTowards(tempNeedle.rotation, Quaternion.Euler(0, 0, -205), Time.deltaTime * speed);
    }

    public void CheckTemp()
    {
        /*if (temperature == 0)
            tempText.text = "Low";
        else if (temperature == 1)
            tempText.text = "Med";
        else if (temperature == 2)
            tempText.text = "High";*/

        if (tempNeedle.eulerAngles.z >= 275)
        {
            temperature = 0;
            Debug.Log("Low");
        }
        else if (tempNeedle.eulerAngles.z <= 190)
        {
            temperature = 2;
            Debug.Log("Hi");
        }
        else if (tempNeedle.eulerAngles.z > 190 && tempNeedle.eulerAngles.z < 275)
        {
            temperature = 1;
            Debug.Log("Med");
        }
    }

    public void IncreaseTemp()
    {
        increaseTemp = true;
    }

    public void IncreaseStop()
    {
        increaseTemp = false;
        CheckTemp();
    }

    public void DecreaseTemp()
    {
        decreaseTemp = true;
    }

    public void DecreaseStop()
    {
        decreaseTemp = false;
        CheckTemp();
    }

    public int GetTemp()
    {
        return temperature;
    }
}
