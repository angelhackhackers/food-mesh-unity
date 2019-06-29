using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Shelves : MonoBehaviour
{
    [Inject]
    readonly SignalBus signalBus;
    public float targetValue;

    private Vector3 startLocalPosition;

    void Awake()
    {
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
        transform.localPosition =
            new Vector3(transform.localPosition.x, (dragging.vector2.y + 1 )* targetValue, transform.localPosition.z);
    }

    void EndDrag(EndDragSignal endDragSignal)
    {
        if (endDragSignal.DragDirection == DragDirection.UP)
        {
            transform.DOLocalMoveY(targetValue, 1f).SetEase(Ease.InOutQuad).OnComplete(GetPrevious);
        }
        else if (endDragSignal.DragDirection == DragDirection.DOWN)
        {
            transform.DOLocalMoveY(0, 1f).SetEase(Ease.InOutQuad).OnComplete(GetNext);

        }
    }

    void GetPrevious()
    {
    }

    void GetNext()
    {
    }

}
