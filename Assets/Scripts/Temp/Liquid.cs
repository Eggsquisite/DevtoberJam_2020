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

    public LiquidColor startColor;
    private LiquidColor baseColor, updatedColor;

    private void Start()
    {
        liquid = GetComponent<Image>();
        ChangeColor(startColor);
    }

    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            updatedColor = data.pointerDrag.GetComponent<Vials>().GetColor();

            CombineColors(baseColor, updatedColor);
            //ChangeColor(updatedColor);
        }
    }

    private Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }

        result /= aColors.Length;
        return result;
    }

    private void ChangeColor(LiquidColor color)
    {
        baseColor = color;
        liquid.color = (Color32)hueColorValues[color];
        Debug.Log("New color is... " + baseColor);
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
}
