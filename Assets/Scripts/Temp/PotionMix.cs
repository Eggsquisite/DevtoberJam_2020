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
    public Text redVial;
    public Text blueVial;
    public Text yellowVial;

    [TextArea(1, 3)]
    public string[] tasks;

    [Header("Potion Properties")]
    public float maxPotionTime; 
    public float qualityDegrade;
    public float solutionAdd;
    public int redVials;
    public int blueVials;
    public int yellowVials;

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
