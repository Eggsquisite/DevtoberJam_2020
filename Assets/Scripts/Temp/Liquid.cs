using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum LiquidColor { Red, Yellow, Blue, Purple, Green, Orange, Brown };

public class Liquid : MonoBehaviour, IDropHandler
{
    private static Hashtable hueColorValues = new Hashtable {
        {LiquidColor.Red,               new Color32( 254, 9, 0, 255) },
        {LiquidColor.Yellow,            new Color32( 254, 224, 0, 255) },
        {LiquidColor.Blue,              new Color32( 0, 122, 254, 255) },
        {LiquidColor.Purple,            new Color32( 143, 0, 254, 255) },
        {LiquidColor.Green,             new Color32( 0, 254, 111, 255) },
        {LiquidColor.Orange,            new Color32( 254, 161, 0, 255) },
        {LiquidColor.Brown,             new Color32( 125, 60, 30, 255) },
    };

    private Image liquid;

    [Header("Color Change")]
    public float duration;
    public float smoothness;
    public float changeChance;
    public float changeGraceTime;

    Color tmpColor;
    private int colorIndex, prevColorIndex;
    public LiquidColor startColor;
    private LiquidColor baseColor, updatedColor;
    private List<LiquidColor> colorArray = new List<LiquidColor>();

    private float downgrades, downgradeAmount;
    private bool changeColorReady, changingColor, colorLoss;

    private void Start()
    {
        liquid = GetComponent<Image>();
        baseColor = startColor;
        changeColorReady = true;

        liquid.color = (Color32)hueColorValues[baseColor];
        colorArray.Add(LiquidColor.Blue);
        colorArray.Add(LiquidColor.Yellow);
        colorArray.Add(LiquidColor.Red);

        InvokeRepeating("ColorChangeChance", 1f, 1f);
    }


    void ColorChangeChance()
    {
        if (Random.Range(0, 100) <= changeChance && changeColorReady)
        {
            changeColorReady = false;
            CancelInvoke("ResetChangeColor");
            Invoke("ResetChangeColor", changeGraceTime);
            colorIndex = Random.Range(0, colorArray.Count);

            LiquidColor tmpColor = colorArray[colorIndex];
            ChangeColor(tmpColor);
        }
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

    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            updatedColor = data.pointerDrag.GetComponent<Vials>().GetColor();

            CombineColors(baseColor, updatedColor);

            if (baseColor == LiquidColor.Brown) 
                downgrades += downgradeAmount;
                // penalize player and degrade quality of potion
        }
    }

    private void ChangeColor(LiquidColor color)
    {
        tmpColor = (Color32)hueColorValues[baseColor];
        baseColor = color;
        changingColor = true;
        changeColorReady = false;

        CancelInvoke("ResetChangeColor");
        Invoke("ResetChangeColor", changeGraceTime);

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
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            liquid.color = Color.Lerp(tmpColor, (Color32)hueColorValues[baseColor], progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        changingColor = false;
        Debug.Log("Color change finished");
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

    private void OnDisable()
    {
        CancelInvoke("ColorChangeChance");
    }
}
