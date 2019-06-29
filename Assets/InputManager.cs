using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

//Oculus.go
public class InputManager : MonoBehaviour
{
    private Vector2 startTouchPosition;
    [Inject]
    readonly SignalBus signalBus;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            signalBus.Fire(new DraggingSignal() { vector2 = new Vector2(Random.Range(-1, 1), 0) }); ;

        }

        if (OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad))
        {
            signalBus.Fire<StartDragSignal>();
            startTouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        }

        if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
        {
            signalBus.Fire(new DraggingSignal() { vector2 = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad)});
        }

        if (OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad))
        {
            DragDirection dragDirection = DragDirection.NONE;
            Vector2 currentTouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            if (Mathf.Abs(currentTouchPosition.x - startTouchPosition.x) > 0.5f)
            {
                if (currentTouchPosition.x > startTouchPosition.x)
                {
                    dragDirection = DragDirection.RIGHT;
                }
                else
                {
                    dragDirection = DragDirection.LEFT;

                }
            }
            else if (Mathf.Abs(currentTouchPosition.y - startTouchPosition.y) > 0.5f)
            {
                if (currentTouchPosition.y > startTouchPosition.y)
                {
                    dragDirection = DragDirection.UP;
                    
                }
                else
                {
                    dragDirection = DragDirection.DOWN;
                   
                }
            }
            signalBus.Fire(new EndDragSignal() { DragDirection = dragDirection });

        }

        if (OVRInput.Get(OVRInput.Button.Back))
        {
            Application.Quit();
        }
    }
}
