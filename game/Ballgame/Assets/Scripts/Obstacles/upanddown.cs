using DG.Tweening;
using UnityEngine;


public class upanddown : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float movespeed = 1f;
    [SerializeField] private Ease easeType = Ease.InOutSine;

    private Vector3 startPos;

   
    private void Awake()
    {
        startPos = transform.position;

        transform.DOMoveY(startPos.y + moveDistance, movespeed).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }

}
