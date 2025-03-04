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
    [SerializeField] Button unlockAutoShootButton;

    [Header("Text Setting")]
    [SerializeField] TextMeshProUGUI attackUpgradeText;
    [SerializeField] TextMeshProUGUI healthUpgradeText;
    [SerializeField] TextMeshProUGUI attackSpeedClickUpgradeText;
    [SerializeField] TextMeshProUGUI automaticAttackUpgradeText;
    [SerializeField] TextMeshProUGUI regenRateUpgradeText;
    [SerializeField] TextMeshProUGUI automaticAttackUnlockText;

    [Header("Player Upgrade")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;
    [SerializeField] GameObject player5;
    [SerializeField] GameObject player6;

    [Header("Audio Clip")]
    [SerializeField] AudioClip upgradedFx;

    void Start()
    {
        MoneyChecking();
    }

    void Update()
    {
        MoneyChecking();
        FeatureUnlock();
        AllTextCheckingUpgrade();
        ChangePlayer();
    }

    void ChangePlayer()
    {
        if (SaveManager.instance.attackDamage >= 20)
        {
            player1.SetActive(false);
            player2.SetActive(true);
        }

        if (SaveManager.instance.attackDamage >= 30)
        {
            player2.SetActive(false);
            player3.SetActive(true);
        }

        if (SaveManager.instance.attackDamage >= 35 && SaveManager.instance.health >= 235)
        {
            player3.SetActive(false);
            player4.SetActive(true);
        }

        if (SaveManager.instance.attackDamage >= 50 && SaveManager.instance.health >= 240)
        {
            player4.SetActive(false);
            player5.SetActive(true);
        }

        if (SaveManager.instance.attackDamage >= 100 && SaveManager.instance.health >= 300)
        {
            player5.SetActive(false);
            player6.SetActive(true);
        }
    }

    void FeatureUnlock()
    {
        if (unlockedAutomaticShoot == true)
        {
            automaticAttackButton.interactable = false;
        }
        else
        {
            automaticAttackButton.interactable = true;
        }
    }

    void MoneyChecking()
    {
        currMoney = SaveManager.instance.money; // Ambil jumlah uang dari SaveManager
    }

    public void UnlockAutomaticShoot()
    {
        if (currMoney >= 100)
        {
            AudioManager.instance.PlaySound(upgradedFx);
            SaveManager.instance.money -= 100;
            unlockedAutomaticShoot = true;
            Debug.Log("Automatic shoot unlocked!");
            unlockAutoShootButton.interactable = false;
            SaveManager.instance.Save();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UnlockAutomaticShootText()
    {
        automaticAttackUnlockText.text =  $"Unlock Automatic Shoot (100 Money)";
    }

    public void UpgradeAttack()
    {
        if (currMoney >= attackUpgradeCost)
        {
            AudioManager.instance.PlaySound(upgradedFx);
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
            AudioManager.instance.PlaySound(upgradedFx);
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
            AudioManager.instance.PlaySound(upgradedFx);
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
            AudioManager.instance.PlaySound(upgradedFx);
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
            AudioManager.instance.PlaySound(upgradedFx);
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
        UnlockAutomaticShootText();
    }
}
