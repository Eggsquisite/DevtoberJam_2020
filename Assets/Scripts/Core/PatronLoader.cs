using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatronLoader {

    private static StreamReader inputFile;
    private static string line;
    
    public static List<Patron> patrons = new List<Patron>();

    public static void LoadDataFromFile() {
        inputFile = new StreamReader("Assets/Scripts/Core/PatronInput.txt");
 //       int counter = 0;
        while ((line = inputFile.ReadLine()) != null) {
            if (line.Contains("//")) line = line.Split('/')[0];
            //Debug.Log(line);
            while ((line != null) && line.Contains("[") && line.Contains("]")) {
                if (line.Contains("FailDialogue")) SetGenericDialogue(false);
                else if (line.Contains("SuccessDialogue")) SetGenericDialogue(true);
                else if (line.Contains("Patron")) AddPatron();
                else line = inputFile.ReadLine();
            }
            /*counter++;
            if (counter > 1000) break;*/
        }

        string s = "We loaded " + patrons.Count + " patrons:\n";
        for (int i = 0; i < patrons.Count; i++) {
            s += patrons[i] + "\n";
        }
        Debug.Log(s);

        Patron.patrons = patrons;
        inputFile.Close();
    }

    private static void SetGenericDialogue(bool success) {
        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")) {
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Trim().Length > 0) {
                if (line.Contains("Response=")) {
                    if (success) DialogueManager.genericWinResponse = line.Split('=')[1].Trim();
                    else DialogueManager.genericFailResponse = line.Split('=')[1].Trim();
                }
            }
        }
    }

    public static void AddPatron() {

        Patron patron = new Patron();
        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")) {
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Trim().Length > 0) {
                if (line.Contains("Name=")) {
                    patron.patron_name = line.Split('=')[1].Trim();
                }
                else if (line.Contains("Costume=")) { }
                else if (line.Contains("Problem=")) {
                    //patron.problem = SplitSentences(line.Split('=')[1].Trim());
                    patron.problem = line.Split('=')[1].Trim();
                }
                else if (line.Contains("Formula=")) {
                    
                }
                else if (line.Contains("Solution=")) {
                    PotionSolution ps = new PotionSolution();
                    string[] s = line.Split(new char[] {'=', ';'});

                    ps.potion = Potion.FindPotion(s[1].Trim());
                    ps.payment = ushort.Parse(s[2].Trim());
                    if (s[3].Contains("yes")) ps.isDrunkImmediately = true;
                    else ps.isDrunkImmediately = false;
                    //ps.comment = SplitSentences(s[4]);
                    ps.comment = s[4].Trim();
                    patron.potionSolutions.Add(ps);
                }
            }
        }
        patrons.Add(patron);
    }
    
    public static string[] SplitSentences(string dialogue) {
        string[] s = dialogue.Split(new char[]{'.','!','?'});
        for (int i = 0; i < s.Length; i++) {
            s[i] = s[i].Trim();
        }
        return s;
    }
}
