using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Plate : MonoBehaviour
{
    [Inject]
    readonly SignalBus signalBus;
    public float EdgeValue;

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
        Debug.Log("x+ " + dragging.vector2.x);
        transform.localPosition =
            new Vector3(dragging.vector2.x * EdgeValue, transform.localPosition.y, transform.localPosition.z);
    }

    void EndDrag()
    {
        transform.localPosition = startLocalPosition;
    }

}
