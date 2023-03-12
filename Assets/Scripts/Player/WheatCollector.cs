using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheatCollector : MonoBehaviour
{
    [Header("Wheat")]
    [SerializeField] int currentWheatBlockCount;
    [SerializeField] int maxWheatBlockCount;
    [SerializeField] private int deliveredCount;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private Rigidbody stackRB;
    [SerializeField] private float offset;
    [SerializeField] private float deliverOffset;

    [SerializeField] private List<GameObject> wheats = new List<GameObject>();

    [SerializeField] private Transform[] deliverPositions;
    [SerializeField] private int deliverPositionCounter;

    [Header("Dependency")]
    [SerializeField] private DependencyManager dependencyManager;
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;

    void Start()
    {
        dependencyManager = FindObjectOfType<DependencyManager>();
        player = dependencyManager.GetPlayer();
        uiManager = dependencyManager.GetUIManager();
    }

    public void CollectWheat(WheatBlock wheatBlock)
    {
        currentWheatBlockCount++;
        uiManager.WheatBlockCountIncrease(currentWheatBlockCount);
        offset += 0.1f;
        wheats.Add(wheatBlock.gameObject);
        wheatBlock.transform.SetParent(stackPosition);
        wheatBlock.transform.DOLocalJump(new Vector3(0,offset,0), 3,1,0.5f);
        wheatBlock.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void DeliverWheats()
    {
        int i = 0;
        while (i < wheats.Count)
        {
            
            GameObject wheatObject = wheats[i];
            wheatObject.transform.SetParent(deliverPositions[deliverPositionCounter]);
            wheatObject.transform.rotation = deliverPositions[deliverPositionCounter].rotation;
            deliveredCount++;
            wheatObject.transform.DOJump(deliverPositions[deliverPositionCounter].position + new Vector3(0,deliverOffset,0), 3f, 1, 0.2f).SetDelay(i * 0.2f).OnComplete(() =>
            {
                currentWheatBlockCount--;
                uiManager.WheatBlockCountDecrease(currentWheatBlockCount);
            });

            i++;

            if (deliverPositionCounter < 3)
            {
                deliverPositionCounter++;
            }
            else
            {
                deliverPositionCounter = 0;
                deliverOffset += 0.1f;
            }



        }
        
        offset = 0;
        StartCoroutine(SellWheats());
    }

    IEnumerator SellWheats()
    {
        yield return new WaitForSeconds(1f);
        uiManager.AnimateSelling(deliveredCount);


        wheats.Clear();
        deliveredCount = 0;
    }

    public int GetCurrentBlockCount()
    {
        return currentWheatBlockCount;
    }

    public int GetMaxBlockCount()
    {
        return maxWheatBlockCount;
    }

    public void SetMaxBlockCount(int _maxBlockCount)
    {
        maxWheatBlockCount= _maxBlockCount;
    }

}
