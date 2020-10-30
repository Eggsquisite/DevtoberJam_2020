using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronManager : MonoBehaviour {

    public List<Patron> patronQueue;
    private static float formulaPass = 0.65f;
    private static float[] methodWeights = new float[] {0f, 0.333f, 0.666f}; // potion solution, stored potion method, formula method
    private int position;
    private Patron currentPatron;
    private bool readyToAccept = true;
    private bool clickToAdvance = true;

    public DialogueManager dm;

    void Start() {

        dm = GameObject.Find("PatronTextBox").GetComponent<DialogueManager>();
        
        PatronShuffle();
        
        LoadNextPatron();
        
    }

    void Update() {
        
    }

    /**
     * This method runs a check to see what Potion solutions are available for each patron, and whether the
     * player has the correct ingredients able to craft that potion
     */
    void LoadNextPatron() {
        List<int> order = new List<int>(3);

        for (int i = 0; i < 3; i++) { //loop 3 times
            float rand = Random.value; 
            int value = -1; 
            for (int j = 0; j < methodWeights.Length - 1; j++) {
                if (rand > methodWeights[j] && rand < methodWeights[j + 1]) value = j;
            }

            if (value == -1) value = methodWeights.Length - 1;
            bool keep = true;
            for (int j = 0; j < order.Count; j++) {
                if (value == order[j]) {
                    keep = false;
                    i--;
                    break;
                }
            }
            if (keep) order.Add(value);
        }

        /*string s = "Method Order:  ";
        for (int i = 0; i < order.Count; i++) {
            s += order[i] + "   ";
        }
        Debug.Log(s);*/
        int index = -1;
        for (int i = 0; i < order.Count; i++) {
            if (order[i] == 0) index = UsePotionSolutionMethod();
            else if (order[i] == 1) index = UseStoredPotionMethod();
            else if (order[i] == 2) index = UseFormulaMethod();
            if (index >= 0) break;
        }

        if (index < 0) index = 0;

        //patronQueue[index]; //NEXT PATRON TO LOAD
        patronQueue[index].visitNumber = position;
        position++;
        currentPatron = patronQueue[index];
        
        
        //move to the back of the queue
        Patron temp = patronQueue[index];
        patronQueue.RemoveAt(index);
        patronQueue.Add(temp);

        StartCoroutine(BeginTransition());

        /*GameObject.Find("Patron").GetComponent<PatronGO>().MakePatronAppear();
        dm.SetName(currentPatron.patron_name);
        dm.SetDialogue(currentPatron.problem);*/
    }

    public IEnumerator BeginTransition() {
        PatronGO go = GameObject.Find("Patron").GetComponent<PatronGO>();
        go.MakePatronAppear();
        while (go.transitioning) {
            yield return null;
        }
        dm.SetName(currentPatron.patron_name);
        dm.SetDialogue(currentPatron.problem);
    }

    public bool GivePatronPotion(Potion potion) {
        if (readyToAccept) {
            bool found = false;
            for (int i = 0; i < currentPatron.potionSolutions.Count; i++) {
                if (currentPatron.potionSolutions[i].potion == potion) {
                    found = true;
                    dm.SetDialogue(currentPatron.potionSolutions[i].comment);
                    Inventory.AddFunds(currentPatron.potionSolutions[i].payment);
                    break;
                }
            }

            if (!found) { // the potion wasn't explicitly listed as a solution so now try the formula approach
                float value = CheckEffectivenessFormula(currentPatron, potion);
                if (value >= formulaPass) { //success
                    dm.SetDialogue(DialogueManager.genericWinResponse);
                    Inventory.AddFunds(200); //CHANGE THIS NUMBER
                }
                else {
                    dm.SetDialogue(DialogueManager.genericFailResponse);
                }
            }

            clickToAdvance = true;
            return true;
        }
        return false;
    }

    public void ClickToAdvance() {
        if (clickToAdvance) {
            StartCoroutine(BeginFade());
        }
    }

    public IEnumerator BeginFade() {
        PatronGO go = GameObject.Find("Patron").GetComponent<PatronGO>();
        dm.ResetAll();
        go.MakePatronDisappear();
        while (go.transitioning) {
            yield return null;
        }
    }


    /*private void UseRandomPatronMethod() {

            int index = (int) Random.Range(0, patronQueue.Count);
            if (index == patronQueue.Count) index--;
        
    }*/

    private int UsePotionSolutionMethod() {
        for (int i = 0; i < patronQueue.Count; i++) {
            for (int j = 0; j < patronQueue[i].potionSolutions.Count; j++) {
                if (patronQueue[i].potionSolutions[j].potion.CanBeCrafted()) return i;
            }
        }

        return -1;
    }
    
    private int UseStoredPotionMethod() {

        for (int i = 0; i < Inventory.potionsInInventory.Count; i++) {
            for (int j = 0; j < patronQueue.Count; j++) {
                for (int k = 0; k < patronQueue[j].potionSolutions.Count; k++) {
                    if (Inventory.potionsInInventory[i] == patronQueue[j].potionSolutions[k].potion) return j;
                }
            }
        }

        return -1;
    }
    private int UseFormulaMethod() {
        for (int i = 0; i < patronQueue.Count; i++) {
            for (int j = 0; j < RecipeBook.potionsInRecipeBook.Count; j++) {
                if (RecipeBook.potionsInRecipeBook[j].CanBeCrafted() &&
                    CheckEffectivenessFormula(patronQueue[i], RecipeBook.potionsInRecipeBook[j]) >= formulaPass) return i;
            }
        }

        return -1;
    }

    public static float CheckEffectivenessFormula(Patron patron, Potion potion) {
        float success = 0;
        for (int i = 0; i < patron.formula.Count; i++) {
            float weight = 0f;
            for (int j = 0; j < potion.attributes.Count; j++) {
                if (patron.formula[i] == potion.attributes[j]) {
                    weight = patron.formula[i].weight / potion.attributes[j].weight;
                    break;
                }
            }
            success += weight;
        }
        success /= patron.formula.Count;
        return success;
    }

    public void PatronShuffle() {
        int noOfPatrons = Patron.patrons.Count;
        patronQueue = new List<Patron>(noOfPatrons);
        
        List<int> tempList = new List<int>(noOfPatrons);
        for (int i = 0; i < noOfPatrons; i++) {
            tempList.Add(i);
        }

        while (patronQueue.Count != noOfPatrons) {
            int rand = (int) Random.Range(0, tempList.Count);
            if (rand == tempList.Count) rand--;
            patronQueue.Add(Patron.patrons[tempList[rand]]);
            tempList.RemoveAt(rand);
        }
    }
}
