using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HumanSettlement : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ClumsyDictionary<HumanSO, int> dwellers;
    private SettlementStats stats;
    private ISettlement settlement;

    [Header("Loot")]
    [SerializeField] private int coin;

    private void Start()
    {
        GameManager.Instance.AddSettlement(this);

        List<Human> spawnedHumans = new List<Human>();
        foreach (HumanSO so in dwellers.Keys)
        {
            for (int i = 0; i < dwellers[so]; i++)
            {
                Human human = Instantiate(so.Prefab, spawnPoint.position, Quaternion.identity).GetComponent<Human>();
                human.Initialize(so);
                spawnedHumans.Add(human);
            }
        }

        stats = GetComponent<SettlementStats>();
        stats.Initialize(this);

        settlement = GetComponent<ISettlement>();
        if(settlement != null)
            settlement.SettlersSpawned(spawnedHumans);
    }

    public void SettlementDestroyed()
    {
        GameManager.Instance.RemoveSettlement(this);
        PlayerManager.Instance.AddCoin(coin);

        if (settlement != null)
            settlement.SettlementDestroyed();
    }
}