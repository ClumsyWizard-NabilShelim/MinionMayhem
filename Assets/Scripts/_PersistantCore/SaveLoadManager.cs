using System.Collections;
using UnityEngine;

public enum SaveKeys
{
    Player_BloodCrystals,
    Player_Coins,
    StructuresDestroyed,
    HumansKilled,
    MinionsLost
}

public static class SaveLoadManager
{
    public static void SaveInt(SaveKeys key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }
    public static int LoadInt(SaveKeys key)
    {
        return PlayerPrefs.GetInt(key.ToString(), 0);
    }
}