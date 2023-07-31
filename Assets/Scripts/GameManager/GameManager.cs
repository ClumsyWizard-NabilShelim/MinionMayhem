using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<HumanSettlement> settlements = new List<HumanSettlement>();
    private List<Human> humans = new List<Human>();
    private List<Minion> minions = new List<Minion>();

    [SerializeField] private string nextLevel;

    //Stats tracking
    private int settlementsDestoryed;
    private int humansKilled;
    private int minionsDied;
    private int bloodCrystalsEarned;
    private int coinsEarned;

    [SerializeField] private Vector2 settlementArea;
    [SerializeField] private Transform[] escapeToPoints;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lostPanel;

    [Header("Tracked Stats")]
    [SerializeField] private TextMeshProUGUI labels;
    [SerializeField] private TextMeshProUGUI values;

    //Adding
    public void AddSettlement(HumanSettlement settlement)
    {
        settlements.Add(settlement);
    }
    public void AddHuman(Human human)
    {
        humans.Add(human);
    }
    public void AddMinion(Minion minion)
    {
        minions.Add(minion);
    }
    //Removing
    public void RemoveSettlement(HumanSettlement settlement)
    {
        settlementsDestoryed++;
        settlements.Remove(settlement);
        CheckVerdict();
    }
    public void RemoveHuman(Human human)
    {
        humansKilled++;
        humans.Remove(human);
        CheckVerdict();
    }
    public void RemoveMinion(Minion minion)
    {
        minionsDied++;
        minions.Remove(minion);
        CheckVerdict();
    }
    public void AddCoinsEarned(int amount)
    {
        coinsEarned += amount;
    }
    public void BloodCrystalsEarned(int amount)
    {
        bloodCrystalsEarned += amount;
    }

    //Checking
    private void CheckVerdict()
    {
        if(minions.Count == 0)
        {
            GameLost();
        }
        if(settlements.Count == 0 && humans.Count == 0)
        {
            GameWon();
        }
    }
    private void GameLost()
    {
        lostPanel.SetActive(true);
    }
    private void GameWon()
    {
        winPanel.SetActive(true);

        float bloodCrystalsRegained = 0;
        for (int i = 0; i < minions.Count; i++)
        {
            bloodCrystalsRegained += ((MinionSO)minions[i].Data).BloodCost * 0.25f;
        }

        PlayerManager.Instance.AddBloodCrystals(Mathf.RoundToInt(bloodCrystalsRegained));

        labels.text = (
            "Structures: \n" +
            "Humans: \n" +
            "Minions Lost: \n" +
            "Blood Crystals: \n"
        );

        values.text = (
            $"{settlementsDestoryed} \n" +
            $"{humansKilled} \n" +
            $"{minionsDied} \n" +
            $"{bloodCrystalsEarned} \n"
        );

        int saveStructuresDestroyed = SaveLoadManager.LoadInt(SaveKeys.StructuresDestroyed);
        int saveHumansKilled = SaveLoadManager.LoadInt(SaveKeys.HumansKilled);
        int saveMinionsLost = SaveLoadManager.LoadInt(SaveKeys.MinionsLost);

        saveStructuresDestroyed += settlementsDestoryed;
        saveHumansKilled += humansKilled;
        saveMinionsLost += minionsDied;

        SaveLoadManager.SaveInt(SaveKeys.StructuresDestroyed, saveStructuresDestroyed);
        SaveLoadManager.SaveInt(SaveKeys.HumansKilled, saveHumansKilled);
        SaveLoadManager.SaveInt(SaveKeys.MinionsLost, saveMinionsLost);
    }

    //UI
    public void MainMenu()
    {
        SceneManagement.Instance.Load("MainMenu");
    }
    public void Continue()
    {
        SceneManagement.Instance.Load(nextLevel);

    }
    public void Retry()
    {
        SceneManagement.Instance.Reload();
    }
    public void Quit()
    {
        Application.Quit();
    }

    //Helper Functions
    public Vector2 GetRandomPoint()
    {
        return new Vector2(Random.Range(-settlementArea.x / 2.0f, settlementArea.x / 2.0f), Random.Range(-settlementArea.y / 2.0f, settlementArea.y / 2.0f));
    }
    public Vector2 GetEscapeToPoint()
    {
        return escapeToPoints[Random.Range(0, escapeToPoints.Length)].position;
    }

    //Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, settlementArea);
    }
    protected override void CleanUpStaticData()
    {
    }
}