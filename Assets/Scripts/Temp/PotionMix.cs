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
    public Notepad tasks;

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
}
