using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionMix : MonoBehaviour
{
    [Header("Game Management")]
    private float totalTime;
    private bool startGame, endGame;

    [Header("Scripts")]
    public Solution sol;
    public Mix mix;
    public Temp temp;

    [Header("Task Management")]
    public GameObject taskParent;
    public Text redVialText;
    public Text blueVialText;
    public Text yellowVialText;

    private int redVialCount, blueVialCount, yellowVialCount;

    [TextArea(1, 3)]
    public string[] tasks;

    [Header("Potion Properties")]
    public float maxPotionTime; 
    public float qualityDegrade;
    public float solutionAdd;
    public int redVialsMax;
    public int blueVialsMax;
    public int yellowVialsMax;

    private float potionQuality;
    

    // Start is called before the first frame update
    void Start()
    {
        sol.SetSolutionAdd(solutionAdd);

        UpdateVials();
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
            EndGame();
        else if (startGame)
            BeginTimer();
        //else
            //return;

        if (sol.GetVialUpdated())
            UpdateVials();
    }

    private void BeginTimer()
    {
        totalTime += Time.deltaTime;
    }

    private void EndGame()
    {
        endGame = false;
        startGame = false;
        Debug.Log("Total time: " + totalTime);
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
        //txt.text = all;
    }
}
