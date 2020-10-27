using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [Header("Minigame Scripts")]
    public Mix m_mix;
    public Liquid m_liquid;
    public Burner m_burner;

    [Header("Potion Properties")]
    public float potionMaxTime;
    public bool start;

    private bool enable;
    private int downgrades, quality;
    private float potionTimer, potionProgress, mixing, liquid;

    // Start is called before the first frame update
    void Start()
    {
        enable = true;
        EnableScripts(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            Begin();

        if (m_mix.GetMixStatus())
            Debug.Log("Mixing...");

        if (m_liquid.GetDowngrades() > downgrades)
            UpdateDowngrades();
    }

    private void EnableScripts(bool status)
    {
        m_mix.enabled = status;
        m_liquid.enabled = status;
        m_burner.enabled = status;
    }

    private void Begin()
    {
        if (enable) { 
            EnableScripts(true);
            enable = false;
        }
    }

    private void UpdateDowngrades()
    {
        downgrades = m_liquid.GetDowngrades();
        Debug.Log("Number of downgrades: " + downgrades);
    }
}
