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

    public static bool textScollingFinished = true;
    private bool rollText = false;
    
    private string dialogue;
    private string displayingText;

    
    void Start() {
        patronName = transform.Find("PatronName").GetComponent<TextMeshProUGUI>();
        patronText = transform.Find("PatronText").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator RollText() {
        int counter = 0;
        int noOfChars = dialogue.Length;
        textScollingFinished = false;
        while (counter != noOfChars) {
            displayingText += dialogue[counter];
            patronText.SetText(displayingText);
            counter++;
            yield return new WaitForSeconds(0.02f);
        }

        textScollingFinished = true;
    }


    public void SetName(string name) {
        patronName.SetText(name);
    }
    public void SetDialogue(string text) {
        dialogue = text;
        patronText.SetText("");
        displayingText = "";
        StartCoroutine("RollText");
    }

    public void ResetAll() {
        patronName.SetText("");
        patronText.SetText("");
    }

    public void OnPointerClick(PointerEventData eventData) {
        /*Debug.Log("POINTER CLICKED");
        if (clickToAdvance) {
            GameObject.Find("PatronManager").GetComponent<PatronManager>().ClickToAdvance();
        }*/
    }
}
