using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public GameObject wheatPrefab;
    [SerializeField] private float respawnDelay;
    [SerializeField] private float growDelay;
    [SerializeField] private bool canRespawn;

    void Start()
    {
        canRespawn = true;
        StartCoroutine(InstantiateWheat(0.2f,3f));
    }

    public void RespawnWheat()
    {
        if (!canRespawn) return;
        canRespawn = false;
        StartCoroutine(InstantiateWheat(respawnDelay,growDelay));
    }

    IEnumerator InstantiateWheat(float _respawnDelay,float _growDelay)
    {
        yield return new WaitForSeconds(_respawnDelay);
        var f = Instantiate(wheatPrefab, transform);
        f.transform.DOScale(1, _growDelay);
        yield return new WaitForSeconds(_growDelay);
        f.GetComponent<SphereCollider>().enabled= true;
        f.GetComponentInChildren<BoxCollider>().enabled= true;
        canRespawn = true;
    }

    public void SetGrowDelay(float _growDelay)
    {
        growDelay= _growDelay;
    }
}
