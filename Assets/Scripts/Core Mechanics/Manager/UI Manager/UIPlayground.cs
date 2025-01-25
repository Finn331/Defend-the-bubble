using System.Collections;
using System.Collections.Generic;
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

    void InitialOpenAnimation()
    {
        // Play initial open animation for UI elements
        LeanTween.scale(scroll, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBack);
    }

    public void Build()
    {
        // Open Build UI and close others
        isBuildUIOpen = true;
        isUpgradeUIOpen = false;
        isFeatureUIOpen = false;

        ToggleUpgradeElements(false);
        UpdateUIState();
    }

    public void Upgrade()
    {
        // Open Upgrade UI and close others
        isUpgradeUIOpen = true;
        isBuildUIOpen = false;
        isFeatureUIOpen = false;

        // Activate all upgrade elements (buttons/texts)
        ToggleUpgradeElements(true);

        UpdateUIState();
    }

    public void Feature()
    {
        // Open Feature UI and close others
        isFeatureUIOpen = true;
        isBuildUIOpen = false;
        isUpgradeUIOpen = false;

        ToggleUpgradeElements(false);
        UpdateUIState();
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
