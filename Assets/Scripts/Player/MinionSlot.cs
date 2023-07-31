using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinionSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Image portrait;
    private Action<MinionSO> onClick;
    private MinionSO data;
    public void Initialize(MinionSO so, Action<MinionSO> onClick)
    {
        data = so;
        this.onClick = onClick;

        amount.text = so.BloodCost.ToString();
        portrait.sprite = so.Portrait;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke(data);
    }
}
