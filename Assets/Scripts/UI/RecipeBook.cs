using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour {
    
    
    
    private Image icon1, icon2, icon3;

    void Start() {

        icon1 = transform.Find("Icon1").GetComponent<Image>();
        icon2 = transform.Find("Icon2").GetComponent<Image>();
        icon3 = transform.Find("Icon3").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipPage(bool fwd) {
        
    }
}
