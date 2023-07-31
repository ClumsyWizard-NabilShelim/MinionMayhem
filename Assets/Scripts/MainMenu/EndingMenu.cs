using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labels;
    [SerializeField] private TextMeshProUGUI values;

    private void Start()
    {
        labels.text = (
            "Structures: \n" +
            "Humans: \n" +
            "Minions Lost: \n"
        );

        values.text = (
            $"{SaveLoadManager.LoadInt(SaveKeys.StructuresDestroyed)} \n" +
            $"{SaveLoadManager.LoadInt(SaveKeys.HumansKilled)} \n" +
            $"{SaveLoadManager.LoadInt(SaveKeys.MinionsLost)} \n"
        );
    }

    public void MainMenu()
    {
        SceneManagement.Instance.Load("MainMenu");
    }
}
