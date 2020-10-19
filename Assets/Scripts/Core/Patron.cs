using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour {
    
    public string patron_name;
    public string costume;
    public string problem;
    public List<WeightedAttribute> attributes = new List<WeightedAttribute>(1);
    public List<PotionSolution> potionSolutions = new List<PotionSolution>(0);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

