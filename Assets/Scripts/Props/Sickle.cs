using DynamicMeshCutter;
using System.Collections;
using UnityEngine;

public class Sickle : CutterBehaviour
{
    [SerializeField] private GameObject wheatBlockPrefab;
    [SerializeField] private bool canCut;
    [SerializeField] private GameObject sickFX;

    public void Cut(MeshTarget target)
    {
        if (!canCut) return;
        Cut(target, transform.position, transform.forward, null, OnCreated);
    }

    public void SetCanCut(bool _canCut)
    {
        canCut = _canCut;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!canCut) return;
        bool target = collision.gameObject.TryGetComponent(out MeshTarget isTarget);

        if (target)
        {
            isTarget.gameObject.GetComponentInParent<Wheat>().RespawnWheat();
            Cut(isTarget);
            sickFX.transform.position = isTarget.transform.position;
            sickFX.SetActive(true);
        }
    }

    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);

        StartCoroutine(DestroyCuttedWheats(cData));
        
    }

    IEnumerator DestroyCuttedWheats(MeshCreationData data)
    {
        for (int i = 0; i < data.CreatedObjects.Length; i++)
        {
            data.CreatedObjects[i].transform.GetChild(0).gameObject.layer = 4;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < data.CreatedObjects.Length; i++)
        {
            Destroy(data.CreatedObjects[i].gameObject);
        }
        sickFX.SetActive(false);
      Instantiate(wheatBlockPrefab, data.CreatedObjects[0].gameObject.transform.position, Quaternion.identity);
    }


}
