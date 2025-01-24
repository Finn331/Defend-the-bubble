using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    //What we want to save
    [Header("Level Unlocked")]
    public bool midGame;
    public bool lateGame;

    [Header("Currency")]
    public int money;

    [Header("Player Stats")]
    public float health;
    public float attackSpeedClick;
    public float attackDamage;
    public float regenRate;
    public float autoFireRate;

    [Header("Initial Screen Setup")]
    public bool isFirstTime = true;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            // Player Stats Save
            health = data.health;
            attackDamage = data.attackDamage;
            regenRate = data.regenRate;
            autoFireRate = data.autoFireRate;

            // Score Save
            money = data.money;

            // Boolean Save
            midGame = data.midGame;
            lateGame = data.lateGame;
            isFirstTime = data.isFirstTime;

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        // Player Stats Save
        data.health = health;
        data.attackDamage = attackDamage;
        data.regenRate = regenRate;
        data.autoFireRate = autoFireRate;

        // Score Save
        data.money = money;

        // Boolean Save
        data.isFirstTime = isFirstTime;
        data.midGame = midGame;
        data.lateGame = lateGame;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class PlayerData_Storage
{
    // Player Stats Save
    public float health;
    public float attackDamage;
    public float regenRate;
    public float autoFireRate;

    // Score Save
    public int money;

    // Boolean Save
    public bool midGame;
    public bool lateGame;
    public bool isFirstTime;
}