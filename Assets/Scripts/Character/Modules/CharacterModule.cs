using System.Collections;
using UnityEngine;

public abstract class CharacterModule : MonoBehaviour
{
    protected Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public abstract void OnMoveDone();
}