using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public Text timesUp;

    [Header("Minigame Scripts")]
    public Mix m_mix;
    public Liquid m_liquid;
    public Burner m_burner;

    [Header("Potion Properties")]
    public Slider slider;
    public bool start;
    public float potionMaxTime;

    private bool enable;
    private float downgrades;
    private float potionTimer, potionProgress, potionQuality, mixing, liquid;

    // Start is called before the first frame update
    void Start()
    {
        enable = true;
        EnableScripts(false);
        timesUp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
            return;
        else
            Begin();

        UpdateSlider();

        if (m_mix.GetMixStatus())
            Mixing();

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

        if (potionTimer < potionMaxTime)
            potionTimer += Time.deltaTime;
        else if (potionTimer >= potionMaxTime)
            End();
        
    }

    private void End()
    {
        start = false;
        potionTimer = 0;
        EnableScripts(false);
        timesUp.enabled = true;
        potionQuality = Mathf.RoundToInt(potionProgress);

        Debug.Log("Finished potion quality: " + potionQuality);
    }

    private void UpdateSlider()
    {
        potionProgress += Time.deltaTime / potionMaxTime;
        slider.value = potionProgress;
    }

    private void Mixing()
    {
        potionProgress += Time.deltaTime / (potionMaxTime / 5);
    }

    private void UpdateDowngrades()
    {
        downgrades = m_liquid.GetDowngrades();
        potionProgress -= downgrades;
        Debug.Log("Number of downgrades: " + downgrades);
    }
}
