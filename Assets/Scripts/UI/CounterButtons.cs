using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class CounterButtons : MonoBehaviour {

    private float fadeTime = 0.1f;
    private GameObject recipeBookGO, inventoryGO;
    private Button recipeBookButton, inventoryButton;
    private CanvasGroup recipeBookCG, inventoryCG;
    
    
    void Start() {

        recipeBookGO = GameObject.Find("Recipe Book");
        inventoryGO = GameObject.Find("Inventory");
        
        recipeBookButton = transform.Find("RecipeBookButton").GetComponent<Button>();
        recipeBookButton.onClick.AddListener(RecipeBookClicked);
        inventoryButton = transform.Find("InventoryButton").GetComponent<Button>();
        inventoryButton.onClick.AddListener(InventoryClicked);
        
        recipeBookCG = recipeBookGO.GetComponent<CanvasGroup>();
        inventoryCG = inventoryGO.GetComponent<CanvasGroup>();
        
        recipeBookGO.SetActive(false);
        inventoryGO.SetActive(false);
    }

    public void RecipeBookClicked() {
        if (recipeBookGO.activeSelf) {
            //recipeBookGO.SetActive(false);
            StartCoroutine(FadeRecipeBookCoroutine(false));
        }
        else {
            //inventoryGO.SetActive(false);
            //recipeBookGO.SetActive(true);
            if (inventoryGO.activeSelf) StartCoroutine(FadeInventoryCoroutine(false));
            StartCoroutine(FadeRecipeBookCoroutine(true));
        }
    }

    public void InventoryClicked() {
        if (inventoryGO.activeSelf) {
            //inventoryGO.SetActive(false);
            StartCoroutine(FadeInventoryCoroutine(false));
        }
        else {
            //recipeBookGO.SetActive(false);
            //inventoryGO.SetActive(true);
            if (recipeBookGO.activeSelf) StartCoroutine(FadeRecipeBookCoroutine(false));
            StartCoroutine(FadeInventoryCoroutine(true));
        }
    }

    private IEnumerator FadeRecipeBookCoroutine(bool enable) {
        //CanvasGroup cg = recipeBookGO.GetComponent<CanvasGroup>();
        //float transitionTime = 0.1f;
        if (enable) {
            recipeBookCG.alpha = 0f;
            recipeBookGO.SetActive(true);
            while (recipeBookCG.alpha < 1f) {
                recipeBookCG.alpha += (Time.deltaTime / fadeTime);
                yield return null;
            }
        }
        else {
            recipeBookCG.alpha = 1f;
            while (recipeBookCG.alpha > 0f) {
                recipeBookCG.alpha -= (Time.deltaTime / fadeTime);
                yield return null;
            }

            recipeBookGO.SetActive(false);
            recipeBookCG.alpha = 0f;
        }
    }

    private IEnumerator FadeInventoryCoroutine(bool enable) {
        if (enable) {
            inventoryCG.alpha = 0f;
            inventoryGO.SetActive(true);
            while (inventoryCG.alpha < 1f) {
                inventoryCG.alpha += (Time.deltaTime / fadeTime);
                yield return null;
            }
        }
        else {
            inventoryCG.alpha = 1f;
            while (inventoryCG.alpha > 0f) {
                inventoryCG.alpha -= (Time.deltaTime / fadeTime);
                yield return null;
            }

            inventoryGO.SetActive(false);
            inventoryCG.alpha = 0f;
        }
    }

}
