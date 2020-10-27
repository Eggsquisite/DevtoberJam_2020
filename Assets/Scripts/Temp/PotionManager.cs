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
    public float mixingBonus, liquidDowngrade, burnerDowngrade;

    private bool enable, loseQuality, mixing;
    private float downgrades;
    private float potionTimer, potionProgress, potionQuality;

    // Start is called before the first frame update
    void Start()
    {
        enable = true;
        EnableScripts(false);
        timesUp.enabled = false;
        m_liquid.SetDowngrade(liquidDowngrade);
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
        else if (!m_mix.GetMixStatus() && mixing)
            mixing = false;

        if (m_liquid.GetDowngrades() > downgrades)
            UpdateDowngrades();

        if (m_burner.GetLosingQuality())
            LoseQuality();
        else if (!m_burner.GetLosingQuality() && loseQuality)
            loseQuality = false;
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
        {
            // Update timer and potion progress
            potionTimer += Time.deltaTime;

            if (!loseQuality)
            {
                potionProgress += Time.deltaTime / (potionMaxTime / 2);
                Debug.Log("free progress");
            }
        }
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
        slider.value = potionProgress;
    }

    private void Mixing()
    {
        mixing = true;
        potionProgress += Time.deltaTime / (potionMaxTime / mixingBonus);
    }

    private void UpdateDowngrades()
    {
        downgrades = m_liquid.GetDowngrades();
        potionProgress -= downgrades;
        Debug.Log("Number of downgrades: " + downgrades);
    }

    private void LoseQuality()
    {
        loseQuality = true;
        potionProgress -= Time.deltaTime / (potionMaxTime / burnerDowngrade);
    }
}
