using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [Header("Upgrade System")]
    [SerializeField] private int currMoney; // Uang yang dimiliki player
    [SerializeField] private int attackUpgradeCost = 100; // Harga upgrade attack awal
    [SerializeField] private int healthUpgradeCost = 100; // Harga upgrade health awal
    [SerializeField] private int attackSpeedClickUpgradeCost = 100; // Harga upgrade attack speed click awal
    [SerializeField] private int automaticAttackUpgradeCost = 100; // Harga upgrade automatic attack awal
    [SerializeField] private int regenRateUpgradeCost = 100; // Harga upgrade regen rate awal

    [Header("Upgrade Multipliers")]
    [SerializeField] private float upgradeCostMultiplier = 1.05f; // Kenaikan cost 5%
    [SerializeField] private float statIncreaseMultiplier = 1.10f; // Kenaikan stat 10%

    [Header("Unlockables")]
    public bool unlockedAutomaticShoot;

    void Start()
    {
        MoneyChecking();
    }

    void Update()
    {
        MoneyChecking();
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
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeHealth()
    {
        if (currMoney >= healthUpgradeCost)
        {
            SaveManager.instance.money -= healthUpgradeCost;
            SaveManager.instance.health = Mathf.RoundToInt(SaveManager.instance.health * statIncreaseMultiplier);
            healthUpgradeCost = Mathf.RoundToInt(healthUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Health upgraded! New health: {SaveManager.instance.health}, Next upgrade cost: {healthUpgradeCost}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeAttackSpeedClick()
    {
        if (currMoney >= attackSpeedClickUpgradeCost)
        {
            SaveManager.instance.money -= attackSpeedClickUpgradeCost;
            SaveManager.instance.attackSpeedClick *= statIncreaseMultiplier;
            attackSpeedClickUpgradeCost = Mathf.RoundToInt(attackSpeedClickUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Attack speed (click) upgraded! New attack speed: {SaveManager.instance.attackSpeedClick}, Next upgrade cost: {attackSpeedClickUpgradeCost}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeAutomaticAttack()
    {
        if (currMoney >= automaticAttackUpgradeCost)
        {
            SaveManager.instance.money -= automaticAttackUpgradeCost;
            SaveManager.instance.autoFireRate = Mathf.RoundToInt(SaveManager.instance.autoFireRate * statIncreaseMultiplier);
            automaticAttackUpgradeCost = Mathf.RoundToInt(automaticAttackUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Automatic attack upgraded! New damage: {SaveManager.instance.autoFireRate}, Next upgrade cost: {automaticAttackUpgradeCost}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeRegenRate()
    {
        if (currMoney >= regenRateUpgradeCost)
        {
            SaveManager.instance.money -= regenRateUpgradeCost;
            SaveManager.instance.regenRate *= statIncreaseMultiplier;
            regenRateUpgradeCost = Mathf.RoundToInt(regenRateUpgradeCost * upgradeCostMultiplier);
            Debug.Log($"Regen rate upgraded! New rate: {SaveManager.instance.regenRate}, Next upgrade cost: {regenRateUpgradeCost}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
