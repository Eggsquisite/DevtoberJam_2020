using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public Text timesUp;
    public Image progressBar;

    [Header("Minigame Scripts")]
    public Mix m_mix;
    public Liquid m_liquid;
    public Burner m_burner;

    [Header("Potion Properties")]
    public Slider slider;
    public bool start;
    public float potionMaxTime;
    public float mixingBonus, liquidDowngrade, burnerDowngrade, colorDowngrade;

    private bool enable, burnerLoss, colorLoss, mixing;
    private float downgrades;
    private float potionTimer, potionProgress, potionQuality;

    // Start is called before the first frame update
    void Start()
    {
        enable = true;
        potionProgress = 1;
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
        MixStatusCheck();
        BurnerQualityCheck();
        ColorQualityCheck();
        ProgressBarChange();
    }

    public void StartGame()
    {
        start = true;
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

            if ((!burnerLoss || !colorLoss) && potionProgress < 5)
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
        potionQuality = Mathf.Abs(Mathf.RoundToInt(potionProgress));

        if (potionQuality > 5)
            potionQuality = 5;
        else if (potionQuality < 1)
            potionQuality = 1;

        Debug.Log("Finished potion quality: " + potionQuality);
    }

    private void UpdateSlider()
    {
        slider.value = potionProgress;
    }

    private void MixStatusCheck()
    {
        if (m_mix.GetMixStatus())
            Mixing();
        else if (!m_mix.GetMixStatus() && mixing)
            mixing = false;
    }

    private void Mixing()
    {
        mixing = true;
        if (potionProgress < 5)
            potionProgress += Time.deltaTime / (potionMaxTime / mixingBonus);
    }

    private void UpdateDowngrades()
    {
        downgrades = m_liquid.GetDowngrades();
        potionProgress -= downgrades;
        if (potionProgress < 0)
            potionProgress = 0;

        Debug.Log("Number of downgrades: " + downgrades);
    }

    private void BurnerQualityCheck()
    {
        if (m_burner.GetLosingQuality())
            BurnerQualityLoss();
        else if (!m_burner.GetLosingQuality() && burnerLoss)
            burnerLoss = false;
    }

    private void BurnerQualityLoss()
    {
        burnerLoss = true;

        if (potionProgress > 0)
            potionProgress -= Time.deltaTime / (potionMaxTime / burnerDowngrade);
    }

    private void ColorQualityCheck()
    {
        if (m_liquid.GetColorLoss())
            ColorQualityLoss();
        else if (!m_liquid.GetColorLoss() && burnerLoss)
            colorLoss = false;

        if (m_liquid.GetDowngrades() > downgrades)
            UpdateDowngrades();
    }

    private void ColorQualityLoss()
    {
        colorLoss = true;
        Debug.Log("Losing color quality");

        if (potionProgress > 0)
            potionProgress -= Time.deltaTime / (potionMaxTime / colorDowngrade);
    }

    private void ProgressBarChange()
    {
        if (colorLoss || burnerLoss)
            progressBar.color = Color.red;
        else if (!colorLoss && !burnerLoss)
            progressBar.color = Color.white;
    }
}
