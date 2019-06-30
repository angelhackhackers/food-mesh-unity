using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    [Inject]
    readonly SignalBus signalBus;


    public float EdgeValue;
    private Vector3 scale;
    private Vector3 startLocalPosition;

    void Awake()
    {
        scale = transform.localScale;
        startLocalPosition = transform.localPosition;
        //signalBus.Subscribe<StartDragSignal>(FollowDrag);
        signalBus.Subscribe<DraggingSignal>(FollowDrag);
        signalBus.Subscribe<EndDragSignal>(EndDrag);
    }

    void OnDestroy()
    {
        //signalBus.Unsubscribe<StartDragSignal>(FollowDrag);
        signalBus.Unsubscribe<DraggingSignal>(FollowDrag);
        signalBus.Unsubscribe<EndDragSignal>(EndDrag);
    }
    void FollowDrag(DraggingSignal dragging)
    {
        //Debug.Log("x+ " + dragging.vector2.x);
        //transform.localPosition =
        //    new Vector3(dragging.vector2.x * EdgeValue, transform.localPosition.y, transform.localPosition.z);
    }

    void EndDrag(EndDragSignal endDragSignal)
    {
        if (endDragSignal.DragDirection == DragDirection.LEFT)
        {
            transform.DOLocalMoveX(-EdgeValue, 0.5f).SetEase(Ease.InOutQuad).OnComplete(GetPrevious);
        }
        else if (endDragSignal.DragDirection == DragDirection.RIGHT)
        {
            transform.DOLocalMoveX(EdgeValue, 0.5f).SetEase(Ease.InOutQuad).OnComplete(GetNext);
        }
    }

    void GetPrevious()
    {
        transform.DOScale(0, 0.25f).OnComplete(() =>
        {
            transform.localPosition = new Vector3(EdgeValue, transform.localPosition.y, transform.localPosition.z);
            transform.DOScale(scale, 0.25f);
            transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutQuad);
        });
       
    }

    void GetNext()
    {
        transform.DOScale(0, 0.25f).OnComplete(() =>
        {
            transform.localPosition = new Vector3(-EdgeValue, transform.localPosition.y, transform.localPosition.z);
            transform.DOScale(scale, 0.25f);
            transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutQuad);
        });


    }

}
