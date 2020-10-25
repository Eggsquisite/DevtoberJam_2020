using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public Burner m_burner;
    public Mix m_mix;

    private bool vialDrag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeakerEnter()
    {
        //Debug.Log("Beaker entered");
    }

    public void BeakerExit()
    {
        Debug.Log("Beaker exited");
    }

    public void VialDragging(bool status)
    {
        vialDrag = status;
    }
}
