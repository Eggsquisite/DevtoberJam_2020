using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//public enum LiquidColor { Red, Yellow, Blue, Purple, Green, Orange, Brown };

public class Solution : MonoBehaviour, IDropHandler
{
    private static Hashtable hueColorValues = new Hashtable {
        {LiquidColor.Red,               new Color32( 254, 9, 0, 175) },
        {LiquidColor.Yellow,            new Color32( 254, 224, 0, 175) },
        {LiquidColor.Blue,              new Color32( 0, 122, 254, 175) },
        {LiquidColor.Purple,            new Color32( 143, 0, 254, 175) },
        {LiquidColor.Green,             new Color32( 0, 254, 111, 175) },
        {LiquidColor.Orange,            new Color32( 254, 161, 0, 175) },
        {LiquidColor.Brown,             new Color32( 125, 60, 30, 175) },
        {LiquidColor.White,             new Color32( 255, 255, 255, 175) },
    };


    [Header("Color Change")]
    public Image liquid;
    public float colorDuration;
    public float colorSmoothness;
    private float delay = 0f;

    [Header("Add Solution")]
    public Slider liquidAmount;
    public float solutionDuration;
    public float solutionSmoothness;
    private float solutionAdd;
    private int redVials, blueVials, yellowVials;
    private bool vialUpdated;

    Color tmpColor;
    private int colorIndex, prevColorIndex;
    public LiquidColor startColor;
    private LiquidColor baseColor, updatedColor;
    private List<LiquidColor> colorArray = new List<LiquidColor>();

    private float downgrades, downgradeAmount;
    private bool changeColorReady, changingColor, colorLoss;

    private void Start()
    {
        baseColor = LiquidColor.White;
        changeColorReady = true;

        liquid.color = (Color32)hueColorValues[baseColor];
        AddColors();
    }

    void AddColors()
    {
        colorArray.Add(LiquidColor.White);
        colorArray.Add(LiquidColor.Blue);
        colorArray.Add(LiquidColor.Yellow);
        colorArray.Add(LiquidColor.Red);
        colorArray.Add(LiquidColor.Orange);
        colorArray.Add(LiquidColor.Green);
        colorArray.Add(LiquidColor.Purple);
    }

    public bool GetColorLoss()
    {
        return colorLoss;
    }

    private void ResetChangeColor()
    {
        changeColorReady = true;
    }

    public void SetDowngrade(float amt)
    {
        downgradeAmount = amt;
    }

    public void SetSolutionAdd(float amt)
    {
        solutionAdd = amt;
    }

    public bool GetVialUpdated()
    {
        return vialUpdated;
    }

    public void SetVialUpdated(bool status)
    {
        vialUpdated = status;
    }

    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            //data.pointerDrag.GetComponent<Vials>().Animation();
            updatedColor = data.pointerDrag.GetComponent<Vials>().GetColor();

            vialUpdated = true;

            if (updatedColor == LiquidColor.Red)
                redVials++;
            else if (updatedColor == LiquidColor.Blue)
                blueVials++;
            else if (updatedColor == LiquidColor.Yellow)
                yellowVials++;


            StopCoroutine("AddLiquid");
            StartCoroutine("AddLiquid");
            CombineColors(baseColor, updatedColor);

            if (baseColor == LiquidColor.Brown)
                downgrades += downgradeAmount;
            // penalize player and degrade quality of potion
        }
    }

    IEnumerator AddLiquid()
    {
        // animation delay
        yield return new WaitForSeconds(delay);

        var tmp = liquidAmount.value + solutionAdd;
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = solutionSmoothness / solutionDuration; //The amount of change to apply.
        while (progress < 1)
        {
            liquidAmount.value = Mathf.Lerp(liquidAmount.value, tmp, progress);
            progress += increment;
            yield return new WaitForSeconds(solutionSmoothness);
        }
    }

    private void ChangeColor(LiquidColor color)
    {
        tmpColor = (Color32)hueColorValues[baseColor];
        baseColor = color;
        changingColor = true;
        changeColorReady = false;

        //CancelInvoke("ResetChangeColor");
        //Invoke("ResetChangeColor", changeGraceTime);

        if (baseColor != startColor)
            colorLoss = true;
        else if (baseColor == startColor)
            colorLoss = false;

        //liquid.color = (Color32)hueColorValues[baseColor];
        StopCoroutine("LerpColor");
        StartCoroutine("LerpColor");
    }

    IEnumerator LerpColor()
    {
        // animation delay
        yield return new WaitForSeconds(delay);

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = colorSmoothness / colorDuration; //The amount of change to apply.
        while (progress < 1)
        {
            liquid.color = Color.Lerp(tmpColor, (Color32)hueColorValues[baseColor], progress);
            progress += increment;
            yield return new WaitForSeconds(colorSmoothness);
        }

        changingColor = false;
    }

    private void CombineColors(LiquidColor baseColor, LiquidColor newColor)
    {
        //
        //  Red + Blue = Purple
        //  Yellow + Blue = Green
        //  Red + Yellow = Orange
        //

        // Case if pouring the same vial in
        if (baseColor == newColor)
            ChangeColor(newColor);

        if (baseColor == LiquidColor.Red && newColor == LiquidColor.Blue || baseColor == LiquidColor.Blue && newColor == LiquidColor.Red)
            ChangeColor(LiquidColor.Purple);
        else if (baseColor == LiquidColor.Yellow && newColor == LiquidColor.Blue || baseColor == LiquidColor.Blue && newColor == LiquidColor.Yellow)
            ChangeColor(LiquidColor.Green);
        else if (baseColor == LiquidColor.Red && newColor == LiquidColor.Yellow || baseColor == LiquidColor.Yellow && newColor == LiquidColor.Red)
            ChangeColor(LiquidColor.Orange);

        // Complementary colors cancel out
        else if (baseColor == LiquidColor.Purple && newColor == LiquidColor.Yellow)
            ChangeColor(LiquidColor.Brown);
        else if (baseColor == LiquidColor.Orange && newColor == LiquidColor.Blue)
            ChangeColor(LiquidColor.Brown);
        else if (baseColor == LiquidColor.Green && newColor == LiquidColor.Red)
            ChangeColor(LiquidColor.Brown);
        else
            ChangeColor(newColor);
    }

    public float GetDowngrades()
    {
        return downgrades;
    }

    public int GetRedVials()
    {
        return redVials;
    }

    public int GetBlueVials()
    {
        return blueVials;
    }

    public int GetYellowVials()
    {
        return yellowVials;
    }

    private void OnDisable()
    {
        CancelInvoke("ColorChangeChance");
    }
}

