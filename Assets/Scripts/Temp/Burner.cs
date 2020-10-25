using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Burner : MonoBehaviour
{
    public Text tempText;
    public float initialTemp, tempRange;

    [Header("Temperature Sway Properties")]
    public int swayRange;
    public float swayFreqMin, swayFreqMax;

    private float swayFreq, swayTimer;
    private bool sway = true;

    [Header("Temperature Change Properties")]
    public int changeRange;
    public float changeChance, changeFreqMin, changeFreqMax;

    private float changeTimer, changeVal, changeFreq;
    private bool changeTemp;

    [Header("Temperature Reset Properties")]
    public float resetFreq;

    private float resetTimer;
    private bool reset, increaseTemp;

    private float temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = initialTemp;
        tempText.text = temp.ToString();
        swayFreq = Random.Range(swayFreqMin, swayFreqMax);
        changeFreq = Random.Range(changeFreqMin, changeFreqMax);

        InvokeRepeating("ChangeChance", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        tempText.text = temp.ToString();
        ChangeColor();

        if (sway && !reset)
            Sway();

        if (changeTemp)
            ChangeTemp();

        if (reset)
            Resetting();
    }

    void ChangeChance()
    {
        if (Random.Range(0, 100) <= changeChance && !changeTemp && !reset)
        {
            sway = false;
            changeTemp = true;
            changeVal = Random.Range(0, 2);
        }
        else return;
    }

    void Sway()
    {
        if (swayTimer < swayFreq)
            swayTimer += Time.deltaTime;
        else if (swayTimer >= swayFreq)
        {
            // change the temp slightly and reset the frequency of sway
            swayTimer = 0;
            swayFreq = Random.Range(swayFreqMin, swayFreqMax);

            if (temp < initialTemp - swayRange)
                temp += Random.Range(0, swayRange);
            else if (temp > initialTemp + swayRange)
                temp += Random.Range(-swayRange, 0);
            else
                temp += Random.Range(-swayRange, swayRange);
        }
    }

    void ChangeTemp()
    {
        //Debug.Log("Changing temp");
        if (changeTimer < changeFreq)
            changeTimer += Time.deltaTime;
        else if (changeTimer >= changeFreq)
        {
            // change the temp greatly
            changeTimer = 0;
            changeFreq = Random.Range(changeFreqMin, changeFreqMax);

            if (changeVal == 0 && temp < temp + changeRange)
                temp++;
            else if (changeVal == 1 && temp > temp - changeRange)
                temp--;
        }
    }

    void ChangeColor()
    {
        if (temp >= initialTemp + changeRange)
            tempText.color = Color.red;
        else if (temp <= initialTemp - changeRange)
            tempText.color = Color.blue;
        else
            tempText.color = Color.black;
        
    }

    void Resetting()
    {
        if (resetTimer < resetFreq)
            resetTimer += Time.deltaTime;
        else if (resetTimer >= resetFreq)
        {
            resetTimer = 0;

            if (increaseTemp)
                temp++;
            else
                temp--;
        }
    }

    public void ButtonDown(bool status)
    {
        reset = true;
        resetTimer = 0;
        sway = changeTemp = false;
        increaseTemp = status;
    }

    public void ButtonUp()
    {
        sway = true;
        reset = false;
    }
}
