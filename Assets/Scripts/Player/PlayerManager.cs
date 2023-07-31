using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private int startingBloodCrystal;
    [SerializeField] private int startingCoin;

    public int BloodCrystal { get; private set; }
    public int Coin { get; private set; }

    [SerializeField] private List<MinionSO> minions;
    private MinionSO selectedMinion;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI bloodCrystalText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Transform minionSlotHolder;
    [SerializeField] private MinionSlot minionSlot;

    private void Start()
    {
        LoadData();
        UpdateUI();

        SceneManagement.OnSceneLoadTriggered += SaveData;
        InputManager.Instance.OnPointerDown += OnPointerDown;

        selectedMinion = minions[0];

        for (int i = 0; i < minions.Count; i++)
        {
            Instantiate(minionSlot, minionSlotHolder).Initialize(minions[i], OnSlotSelected);
        }
    }

    private void OnSlotSelected(MinionSO so)
    {
        Debug.Log("Selected");
        selectedMinion = so;
    }

    private void OnPointerDown(Vector2 mouseWorldPosition)
    {
        if (!UseBloodCrystal(selectedMinion.BloodCost))
            return;

        Minion minion = Instantiate(selectedMinion.Prefab, mouseWorldPosition, Quaternion.identity).GetComponent<Minion>();
        minion.Initialize(selectedMinion);
        CameraShake.Instance.ShakeObject(0.2f, ShakeMagnitude.Small);
    }

    //Stats
    public void AddBloodCrystals(int amount)
    {
        BloodCrystal += amount;
        GameManager.Instance.BloodCrystalsEarned(amount);
        UpdateUI();
    }
    public bool UseBloodCrystal(int amount)
    {
        if ((BloodCrystal - amount) < 0)
            return false;

        BloodCrystal -= amount;
        UpdateUI();
        return true;
    }

    public void AddCoin(int coin)
    {
        GameManager.Instance.AddCoinsEarned(coin);
        Coin += coin;
        UpdateUI();
    }

    //UI
    private void UpdateUI()
    {
        bloodCrystalText.text = BloodCrystal.ToString();
        coinText.text = Coin.ToString();
    }

    //Saving data
    private void SaveData()
    {
        SaveLoadManager.SaveInt(SaveKeys.Player_BloodCrystals, BloodCrystal);
        SaveLoadManager.SaveInt(SaveKeys.Player_Coins, Coin);
    }
    private void LoadData()
    {
        BloodCrystal = SaveLoadManager.LoadInt(SaveKeys.Player_BloodCrystals);
        if (BloodCrystal < 10) //Minimum crystals needed for one minion
            BloodCrystal = startingBloodCrystal;

        Coin = SaveLoadManager.LoadInt(SaveKeys.Player_Coins);
        if (Coin == 0)
            Coin = startingCoin;
    }

    protected override void CleanUpStaticData()
    {
    }
}
