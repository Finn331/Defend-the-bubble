using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject attackUpgradeText;
    [SerializeField] GameObject healthUpgradeText;
    [SerializeField] GameObject attackSpeedClickUpgradeText;
    [SerializeField] GameObject automaticAttackUpgradeText;
    [SerializeField] GameObject regenRateUpgradeText;
    [SerializeField] GameObject automaticAttackUnlockText;

    [Header("UI Sound Clip")]
    [SerializeField] AudioClip uiClick;

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
        AudioManager.instance.PlaySound(uiClick);
        demolishButton.SetActive(false);
        upgradeButton.SetActive(false);
        buildButton.SetActive(false);
        buildUIContainer.SetActive(false);
        upgradeUIContainer.SetActive(false);
        featureUIContainer.SetActive(false);

        attackUpgradeText.SetActive(false);
        healthUpgradeText.SetActive(false);
        attackSpeedClickUpgradeText.SetActive(false);
        automaticAttackUpgradeText.SetActive(false);
        regenRateUpgradeText.SetActive(false);
        automaticAttackUnlockText.SetActive(false);

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
        AudioManager.instance.PlaySound(uiClick);
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
        AudioManager.instance.PlaySound(uiClick);
        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                // Open Upgrade UI and close others
                isUpgradeUIOpen = true;
                isBuildUIOpen = false;
                isFeatureUIOpen = false;
                DUBButton();

                attackUpgradeText.SetActive(true);
                healthUpgradeText.SetActive(true);
                attackSpeedClickUpgradeText.SetActive(true);
                automaticAttackUpgradeText.SetActive(true);
                regenRateUpgradeText.SetActive(true);

                // Activate all upgrade elements (buttons/texts)
                ToggleUpgradeElements(true);

                UpdateUIState();
            });
        });
    }

    public void Feature()
    {
        AudioManager.instance.PlaySound(uiClick);
        LeanTween.scale(scroll, new Vector3(0.0951583385f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.scale(scroll, new Vector3(1.12370002f, 1.12370002f, 1.12370002f), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                // Open Feature UI and close others
                isFeatureUIOpen = true;
                isBuildUIOpen = false;
                isUpgradeUIOpen = false;
                DUBButton();
                automaticAttackUnlockText.SetActive(true);

                ToggleUpgradeElements(false);
                UpdateUIState();
            });
        });
    }

    public void Retry()
    {
        AudioManager.instance.PlaySound(uiClick);
        SaveManager.instance.health = 100;
        SaveManager.instance.money = 0;
        SaveManager.instance.attackSpeedClick = 2;
        SaveManager.instance.attackDamage = 5;
        SaveManager.instance.regenRate = 0.1f;
        SaveManager.instance.autoFireRate = 0.5f;
        SaveManager.instance.isGameover = false;
        SaveManager.instance.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
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
