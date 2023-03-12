using UnityEngine;

public class WheatBlock : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider myCollider;


    public Rigidbody GetRB()
    {
        return rb;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            rb.isKinematic = true;
            myCollider.isTrigger = true;
        }

    }
}
