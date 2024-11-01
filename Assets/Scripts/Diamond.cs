using DG.Tweening;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField]
    private float rotateDuration = 1f;
    public delegate void DiamondTaken();
    public static event DiamondTaken OnDiamondTaken;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.DOKill();
            OnDiamondTaken?.Invoke();
            gameObject.SetActive(false);
   
        }
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
