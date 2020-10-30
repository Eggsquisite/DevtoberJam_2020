using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron {
    
    public string patron_name;
    public string costume;
    public string problem;
    public List<string> solution; // leave null
    public List<WeightedAttribute> formula = new List<WeightedAttribute>(1);
    public List<PotionSolution> potionSolutions = new List<PotionSolution>(0);
    public bool hasVisitedTheShop = false;
    public int visitNumber = 0;

    public static List<Patron> patrons;

    public Patron() { }

    public override string ToString() {
        string s = "Name: " + patron_name;
        s += "\nCostume: " + costume;
        s += "\nProblem: " + problem;
        s += "\nFormula: ";
        for (int i = 0; i < formula.Count; i++) {
            s += formula[i] + ", ";
        }

        s += "\nSolutions:\n";
        for (int i = 0; i < potionSolutions.Count; i++) {
            s += potionSolutions[i] + "\n";
        }

        return s;
    }
}

/*
public class QueuedPatron {

    public Patron patron;
    public bool hasVisitedTheShop = false;
    public int visitNumber = -1;

    public QueuedPatron(Patron patron) {
        this.patron = patron;
    }
    
}*/

public class PotionSolution {
    public Potion potion { get; set; }
    public ushort payment { get; set; }
    public bool isDrunkImmediately { get; set; }
    public string comment { get; set; }

    public override string ToString() {
        string s = "Potion: " + potion.potion_name;
        s += "\nReward: " + payment;
        s += "\nIs Potion Drunk Immediately?: " + isDrunkImmediately;
        s += "\nDialogue: " + comment;
        return s;
    }
}

public enum PatronStage {
    WAITING_FOR_POTION,
    BLAH
}

