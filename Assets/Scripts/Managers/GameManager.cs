using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager
    [SerializeField] private SettingsSO gameSettings;
    [SerializeField] private DependencyManager dependencyManager;    
    [SerializeField] private Wheat[] wheat;

    private WheatCollector wheatCollector;
    private UIManager uiManager;
    void Start()
    {
        wheatCollector = dependencyManager.GetWheatCollector();
        uiManager= dependencyManager.GetUIManager();
        wheatCollector.SetMaxBlockCount(gameSettings.maxBlockCount);
        uiManager.SetMaxWheatCount(gameSettings.maxBlockCount);

        for (int i = 0; i < wheat.Length; i++)
        {
            wheat[i].SetGrowDelay(gameSettings.wheatGrowDelay);
        }

    }

}
