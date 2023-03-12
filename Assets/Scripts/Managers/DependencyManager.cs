using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private WheatCollector wheatCollector;
    [SerializeField] private Player player;
    [SerializeField] private Sickle sickle;


    public GameManager GetGameManager()
    {
        return gm;
    }

    public UIManager GetUIManager()
    {
        return uiManager;
    }

    public WheatCollector GetWheatCollector()
    {
        return wheatCollector;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Sickle GetSickle()
    {
        return sickle;
    }
}
