using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMix : MonoBehaviour
{

    public float totalTime;
    public bool startGame, endGame;

    [Header("Potion Properties")]
    public float maxPotionTime;
    private float potionQuality;

    // Start is called before the first frame update
    void Start()
    {
            
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
