using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PatronGO : MonoBehaviour {
    
    private Image patronImage;

    private Color32 silhouette = new Color32(0,0,0,150);
    public bool transitioning = false;

    void Start() {
        patronImage = GetComponent<Image>();
        patronImage.sprite = Resources.Load<Sprite>("Art/Characters/character1");
        
    }

    private void OnEnable() {
        patronImage = GetComponent<Image>();
        patronImage.sprite = Resources.Load<Sprite>("Art/Characters/character1");
    }

    public void MakePatronAppear() {
        int number = (int)(Random.value*24 + 1);
        if (number == 25) number--;
        string n = number + "";
        if (number < 10) n = "0" + number;
        patronImage.sprite = Resources.Load<Sprite>("Art/Characters/patron0" + n);
        StartCoroutine(Transition(Color.clear, silhouette, Color.white, 2f, 0.5f));
    }

    public void MakePatronDisappear() {
        StartCoroutine(Transition(Color.white, Color.clear, 1f));
    }

    IEnumerator Transition(Color32 start, Color32 end, float transitionTime) {
        transitioning = true;
        float t = 0f;
        while (t < 1) {
            patronImage.color = Color.Lerp(start, end, t);
            t += (Time.deltaTime / transitionTime);
            yield return null;
        }
        transitioning = false;
    }
    
    IEnumerator Transition(Color32 start, Color32 middle, Color32 end, float transitionTime1, float transitionTime2) {
        transitioning = true;
        float t = 0f;
        while (t < 1f) {
            patronImage.color = Color.Lerp(start, middle, t);
            t += (Time.deltaTime / transitionTime1);
            yield return null;
        }

        t = 0f;
        while (t < 1f) {
            patronImage.color = Color.Lerp(middle, end, t);
            t += (Time.deltaTime / transitionTime2);
            yield return null;
        }
        transitioning = false;
    }
}
