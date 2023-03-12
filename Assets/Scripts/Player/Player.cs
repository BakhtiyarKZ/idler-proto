using DG.Tweening;
using DynamicMeshCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    [SerializeField] MeshRenderer sickleMeshRenderer;
    [SerializeField] BoxCollider sickleCollider;
    [SerializeField] PlayerMovement playerMovement;
    
    List<GameObject> wheatBlocksList = new List<GameObject>();

    [Header("Dependency")]
    [SerializeField] private DependencyManager dependencyManager;
    [SerializeField] private WheatCollector wheatCollector;
    [SerializeField] private GameManager gm;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Sickle sickle;

    [SerializeField] Transform barnPosition;
    [SerializeField] bool canDeliver;
    [SerializeField] bool canSick;

    void Start()
    {
        dependencyManager = FindObjectOfType<DependencyManager>();
        gm = dependencyManager.GetGameManager();
        uiManager= dependencyManager.GetUIManager();
        sickle = dependencyManager.GetSickle();
        canDeliver = true;
        canSick = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheat"))
        {
            if (!canSick) return;
            animator.SetTrigger("Sick");
            playerMovement.SetCanMove(false);
            StartCoroutine(EnableSick());
            StartCoroutine(HideSickle());
        }

        if (other.gameObject.CompareTag("WheatBlock"))
        {
            if (wheatCollector.GetCurrentBlockCount() >= wheatCollector.GetMaxBlockCount()) return;
            other.gameObject.tag = "Untagged";
            WheatBlock wheatBlock = other.gameObject.GetComponent<WheatBlock>();
            wheatCollector.CollectWheat(wheatBlock);
            
        }

        bool isBarn = other.gameObject.TryGetComponent(out Barn barn);

        if (isBarn)
        {
            if (!canDeliver || wheatCollector.GetCurrentBlockCount()==0 ) return;
            canDeliver = false;
            wheatCollector.DeliverWheats();
            StartCoroutine(EnableDelivering());
        }

    }


    IEnumerator EnableDelivering()
    {
        yield return new WaitForSeconds(1f);
        canDeliver = true;
    }

    IEnumerator EnableSick()
    {
        yield return new WaitForSeconds(0.7f);
        canSick= true;
    }

    IEnumerator HideSickle()
    {
        canSick= false;
        sickle.SetCanCut(true);
        
        sickleMeshRenderer.enabled = true;
        sickleCollider.enabled = true;
        
        yield return new WaitForSeconds(0.7f);
        playerMovement.SetCanMove(true);
        sickle.SetCanCut(false);
        sickleMeshRenderer.enabled = false;
        sickleCollider.enabled = false;
        
        
 

    }

}
