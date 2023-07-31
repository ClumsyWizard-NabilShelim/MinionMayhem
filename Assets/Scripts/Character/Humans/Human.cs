using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Human : Character
{
    public override void Initialize(CharacterSO data)
    {
        base.Initialize(data);
        GameManager.Instance.AddHuman(this);
    }
}
