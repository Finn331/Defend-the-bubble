using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [Header("Upgrade System")]
    [SerializeField] private int currMoney; // Uang yang dimiliki player
    [SerializeField] private int attackUpgradeCost = 10; // Harga upgrade attack awal
    [SerializeField] private int healthUpgradeCost = 10; // Harga upgrade health awal
    [SerializeField] private int attackSpeedClickUpgradeCost = 10; // Harga upgrade attack speed click awal
    [SerializeField] private int automaticAttackUpgradeCost = 10; // Harga upgrade automatic attack awal
    [SerializeField] private int regenRateUpgradeCost = 10; // Harga upgrade regen rate awal

    [Header("Upgrade Multipliers")]
    [SerializeField] private float upgradeCostMultiplier = 1.05f; // Kenaikan cost 5%
    [SerializeField] private float statIncreaseMultiplier = 1.10f; // Kenaikan stat 10%

    [Header("Unlockables")]
    public bool unlockedAutomaticShoot;

    [Header("Button Setting")]
    [SerializeField] Button automaticAttackButton;

    [Header("Text Setting")]
    [SerializeField] TextMeshProUGUI attackUpgradeText;
    [SerializeField] TextMeshProUGUI healthUpgradeText;
    [SerializeField] TextMeshProUGUI attackSpeedClickUpgradeText;
    [SerializeField] TextMeshProUGUI automaticAttackUpgradeText;
    [SerializeField] TextMeshProUGUI regenRateUpgradeText;



    void Start()
    {
        MoneyChecking();
    }

    void Update()
    {
        MoneyChecking();
        FeatureUnlock();
        AllTextCheckingUpgrade();
    }

    void FeatureUnlock()
    {
        if (unlockedAutomaticShoot == true)
        {
            automaticAttackButton.interactable = true;
        }
        else
        {
            automaticAttackButton.interactable = false;
        }
    }

    void MoneyChecking()
    {
        currMoney = SaveManager.instance.money; // Ambil jumlah uang dari SaveManager
    }

    public void UpgradeAttack()
    {
        if (currMoney >= attackUpgradeCost)
        {
            SaveManager.instance.money -= attackUpgradeCost;
            SaveManager.instance.attackDamage = Mathf.RoundToInt(SaveManager.instance.attackDamage * statIncreaseMultiplier);
            attackUpgradeCost = Mathf.RoundToInt(attackUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Attack upgraded! New attack damage: {SaveManager.instance.attackDamage}, Next upgrade cost: {attackUpgradeCost}");
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpgradeAttackText()
    {
        attackUpgradeText.text = $"Upgrade Attack ({attackUpgradeCost} Money)";
    }

    public void UpgradeHealth()
    {
        if (currMoney >= healthUpgradeCost)
        {
            SaveManager.instance.money -= healthUpgradeCost;
            SaveManager.instance.health = Mathf.RoundToInt(SaveManager.instance.health * statIncreaseMultiplier);
            healthUpgradeCost = Mathf.RoundToInt(healthUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Health upgraded! New health: {SaveManager.instance.health}, Next upgrade cost: {healthUpgradeCost}");
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpgradeHealthText()
    {
        healthUpgradeText.text = $"Upgrade Health ({healthUpgradeCost} Money)";
    }

    public void UpgradeAttackSpeedClick()
    {
        if (currMoney >= attackSpeedClickUpgradeCost)
        {
            SaveManager.instance.money -= attackSpeedClickUpgradeCost;
            SaveManager.instance.attackSpeedClick *= statIncreaseMultiplier;
            attackSpeedClickUpgradeCost = Mathf.RoundToInt(attackSpeedClickUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Attack speed (click) upgraded! New attack speed: {SaveManager.instance.attackSpeedClick}, Next upgrade cost: {attackSpeedClickUpgradeCost}");
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpgradeAttackSpeedClickText()
    {
        attackSpeedClickUpgradeText.text = $"Upgrade Attack Speed (Click) ({attackSpeedClickUpgradeCost} Money)";
    }

    public void UpgradeAutomaticAttack()
    {
        if (currMoney >= automaticAttackUpgradeCost)
        {
            SaveManager.instance.money -= automaticAttackUpgradeCost;
            SaveManager.instance.autoFireRate = Mathf.RoundToInt(SaveManager.instance.autoFireRate * statIncreaseMultiplier);
            automaticAttackUpgradeCost = Mathf.RoundToInt(automaticAttackUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Automatic attack upgraded! New damage: {SaveManager.instance.autoFireRate}, Next upgrade cost: {automaticAttackUpgradeCost}");
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpgradeAutomaticAttactText()
    {
        automaticAttackUpgradeText.text = $"Upgrade Automatic Attack Firerate ({automaticAttackUpgradeCost} Money)";
    }

    public void UpgradeRegenRate()
    {
        if (currMoney >= regenRateUpgradeCost)
        {
            SaveManager.instance.money -= regenRateUpgradeCost;
            SaveManager.instance.regenRate *= statIncreaseMultiplier;
            regenRateUpgradeCost = Mathf.RoundToInt(regenRateUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Regen rate upgraded! New rate: {SaveManager.instance.regenRate}, Next upgrade cost: {regenRateUpgradeCost}");
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpgradeRegenRateText()
    {
        regenRateUpgradeText.text = $"Upgrade Regen Rate ({regenRateUpgradeCost} Money)";
    }


    void AllTextCheckingUpgrade()
    {
        UpgradeAttackText();
        UpgradeHealthText();
        UpgradeRegenRateText();
        UpgradeAttackSpeedClickText();
        UpgradeAutomaticAttactText();
    }
}
