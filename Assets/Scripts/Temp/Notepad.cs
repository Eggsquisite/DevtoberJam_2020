using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notepad : MonoBehaviour
{
    [Header("Notepad Movement")]
    public Transform outTarget;
    public Transform inTarget;

    public float speed;
    private bool moving, originalPos;

    [Header("Solution Tasks")]
    public Solution sol;

    public Text redVialText;
    public Text blueVialText;
    public Text yellowVialText;

    public int redVialsMax;
    public int blueVialsMax;
    public int yellowVialsMax;

    private int redVialCount, blueVialCount, yellowVialCount;

    [Header("Color Tasks")]
    public Text baseColor;
    public Text finalColor;

    [Header("Ingredient Tasks")]
    private int ingredients;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVials();     
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && originalPos)
            transform.position = Vector2.MoveTowards(transform.position, outTarget.position, speed * Time.deltaTime);
        else if (moving && !originalPos)
            transform.position = Vector2.MoveTowards(transform.position, inTarget.position, speed * Time.deltaTime);

        if (transform.position == outTarget.position || transform.position == inTarget.position)
            moving = false;

        if (sol.GetVialUpdated())
            UpdateVials();
    }

    public void Move()
    {
        moving = true;
        originalPos = !originalPos;
    }

    private void UpdateVials()
    {
        redVialCount = sol.GetRedVials();
        blueVialCount = sol.GetBlueVials();
        yellowVialCount = sol.GetYellowVials();

        UpdateVialTextCount(redVialCount, redVialsMax, redVialText);
        UpdateVialTextCount(blueVialCount, blueVialsMax, blueVialText);
        UpdateVialTextCount(yellowVialCount, yellowVialsMax, yellowVialText);

        sol.SetVialUpdated(false);
    }

    private void UpdateVialTextCount(int currentCount, int maxCount, Text txt)
    {
        string currentVials = $"({currentCount} / ";
        string maxVials = $"{maxCount})";
        string all = string.Concat(currentVials, maxVials);
        txt.text = all;
    }
}
