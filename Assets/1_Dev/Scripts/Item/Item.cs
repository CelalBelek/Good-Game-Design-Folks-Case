using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour, ICollectible
{
    [SerializeField] private float jumpDuration = 0.5f;  
    [SerializeField] private float rayDistance = 10f; 

    public void Collect(Transform otherTransform)
    {
        transform.SetParent(otherTransform); 

        transform.DOLocalMove(Vector2.zero, jumpDuration)
            .SetEase(Ease.OutQuad) 
            .OnComplete(() =>
            {
                transform.localPosition = Vector3.zero; 

                GetComponent<Renderer>().enabled = false;
            });

            InventoryController.Instance.CurrentInventoryAdd(this);
    }

    public Transform ItemTransform
    {
        get { return this.transform; }
    }

    public void Drop()
    {
        transform.SetParent(null); 

        GetComponent<Renderer>().enabled = true;
        DetectGroundAndMove();
    }

    private void DetectGroundAndMove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            transform.DOMoveY(hit.point.y + 0.5f, jumpDuration).SetEase(Ease.OutBounce);
        }
    }
}
