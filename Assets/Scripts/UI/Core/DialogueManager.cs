using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour, IPointerClickHandler {
    
    public static string genericWinResponse, genericFailResponse;
    private TextMeshProUGUI patronName, patronText;
    
    private List<string> sentences;
    private bool clickToAdvanceText = false;
    private bool rollText = false;
    public Patron patron;
    
    private string displayText;
    private int noOfChars;
    
    void Start() {
        sentences = new List<string>();

        patronName = transform.Find("PatronName").GetComponent<TextMeshProUGUI>();
        patronText = transform.Find("PatronText").GetComponent<TextMeshProUGUI>();
        
        
    }

    IEnumerator RollText() {
        int counter = 0;
        while (counter != noOfChars) {
            displayText += sentences[0][counter];
            patronText.SetText(displayText);
            counter++;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void LoadPatron(Patron patron) {
        sentences.Add(patron.problem);
        noOfChars = sentences[0].Length;
        StartCoroutine("RollText");
    }
    
    
    
    
    
    

    public void OnPointerClick(PointerEventData eventData) {
        if (clickToAdvanceText) {
            
        }
    }
}
