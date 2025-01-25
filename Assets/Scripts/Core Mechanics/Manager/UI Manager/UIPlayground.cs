using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayground : MonoBehaviour
{
    [Header("UI Build")]
    [SerializeField] private GameObject buildUIContainer;

    [Header("UI Upgrade")]
    [SerializeField] private GameObject upgradeUIContainer;
    //[SerializeField] private GameObject[] upgradePanelButtons;
    [SerializeField] private GameObject[] upgradePanelTexts;

    [Header("UI Feature")]
    [SerializeField] private GameObject featureUIContainer;
    [SerializeField] private GameObject featureUIPanelText;

    [Header("UI Boolean")]
    private bool isBuildUIOpen;
    private bool isUpgradeUIOpen;
    private bool isFeatureUIOpen;

    [Header("UI Sprite")]
    [SerializeField] GameObject scroll;

    [Header("UI Button")]
    [SerializeField] GameObject demolishButton;
    [SerializeField] GameObject upgradeButton;
    [SerializeField] GameObject buildButton;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI attackUpgradeText;
    [SerializeField] TextMeshProUGUI healthUpgradeText;
    [SerializeField] TextMeshProUGUI attackSpeedClickUpgradeText;
    [SerializeField] TextMeshProUGUI automaticAttackUpgradeText;
    [SerializeField] TextMeshProUGUI regenRateUpgradeText;
    [SerializeField] TextMeshProUGUI automaticAttackUnlockText;
    
    // Start is called before the first frame update
    void Start()
    {
        InitialOpenAnimation();

        // Ensure all UI containers are initially disabled
        buildUIContainer.SetActive(false);
        upgradeUIContainer.SetActive(false);
        featureUIContainer.SetActive(false);

        // Ensure all upgrade elements are initially disabled
        ToggleUpgradeElements(false);
    }

    void DUBButton()
    {
        demolishButton.SetActive(false);
        upgradeButton.SetActive(false);
        buildButton.SetActive(false);
    }

    public void BackUpgrade()
    {
        demolishButton.SetActive(false);
        upgradeButton.SetActive(false);
        buildButton.SetActive(false);
        buildUIContainer.SetActive(false);
        upgradeUIContainer.SetActive(false);
        featureUIContainer.SetActive(false);

        attackUpgradeText.enabled = false;
        healthUpgradeText.enabled = false;
        attackSpeedClickUpgradeText.enabled = false;
        automaticAttackUpgradeText.enabled = false;
        regenRateUpgradeText.enabled = false;
        automaticAttackUnlockText.enabled = false;

        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                demolishButton.SetActive(true);
                upgradeButton.SetActive(true);
                buildButton.SetActive(true);
            });
        });
    }
    void InitialOpenAnimation()
    {
        // Play initial open animation for UI elements
        LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack);
    }

    public void Build()
    {                   
        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                // Open Build UI and close others
                isBuildUIOpen = true;
                isUpgradeUIOpen = false;
                isFeatureUIOpen = false;
                DUBButton();

                ToggleUpgradeElements(false);
                UpdateUIState();
            });
        });        
    }

    public void Upgrade()
    {        
        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                // Open Upgrade UI and close others
                isUpgradeUIOpen = true;
                isBuildUIOpen = false;
                isFeatureUIOpen = false;
                DUBButton();

                // Activate all upgrade elements (buttons/texts)
                ToggleUpgradeElements(true);

                UpdateUIState();
            });
        });
    }

    public void Feature()
    {
        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                // Open Feature UI and close others
                isFeatureUIOpen = true;
                isBuildUIOpen = false;
                isUpgradeUIOpen = false;
                DUBButton();

                ToggleUpgradeElements(false);
                UpdateUIState();
            });
        });
    }

    private void UpdateUIState()
    {
        // Update UI container visibility based on boolean values
        buildUIContainer.SetActive(isBuildUIOpen);
        upgradeUIContainer.SetActive(isUpgradeUIOpen);
        featureUIContainer.SetActive(isFeatureUIOpen);
    }

    private void ToggleUpgradeElements(bool isActive)
    {
        foreach (GameObject text in upgradePanelTexts)
        {
            text.SetActive(isActive);
        }
    }
}
