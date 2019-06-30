﻿using UnityEngine;
using UnityEngine.Events;

public class VRaycaster : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public Transform centerEyeAnchor = null;
    public LineRenderer lineRenderer = null;
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public VRaycaster.Callback raycastHitCallback;
    public GameObject currentHover;

    void Awake()
    {
        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHandAnchor");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("Assign CenterEyeAnchor in the inspector!");
            GameObject center = GameObject.Find("CenterEyeAnchor");
            if (center != null)
            {
                centerEyeAnchor = center.transform;
            }
        }
        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
        }
    }

    Transform Pointer
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.LTrackedRemote) != OVRInput.Controller.None)
            {
                return leftHandAnchor;
            }
            else if ((controller & OVRInput.Controller.RTrackedRemote) != OVRInput.Controller.None)
            {
                return rightHandAnchor;
            }
            // If no controllers are connected, we use ray from the view camera. 
            // This looks super ackward! Should probably fall back to a simple reticle!
            return centerEyeAnchor;
        }
    }

    void Update()
    {
        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }

        Ray laserPointer = new Ray(pointer.position, pointer.forward);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }


        RaycastHit hit;
        if (Physics.Raycast(laserPointer, out hit, maxRayDistance, ~excludeLayers))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            if (currentHover == null )
            {
                currentHover = hit.collider.gameObject;
                if (hit.collider.gameObject.GetComponent<GoogleButton>() != null)
                {
                    hit.collider.gameObject.GetComponent<GoogleButton>().OnHover();
                }
                if (hit.collider.gameObject.GetComponent<DishData>() != null)
                {
                    hit.collider.gameObject.GetComponent<DishData>().OnHover();
                }
                if (hit.collider.gameObject.GetComponent<BuyButton>() != null)
                {
                    hit.collider.gameObject.GetComponent<BuyButton>().OnHover();
                }
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.One) ||
                OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {

                if (hit.collider.gameObject.GetComponent<GoogleButton>() != null)
                {
                    hit.collider.gameObject.GetComponent<GoogleButton>().OnClicked();
                }
                if (hit.collider.gameObject.GetComponent<DishData>() != null)
                {
                    hit.collider.gameObject.GetComponent<DishData>().OnClicked();
                }
                if (hit.collider.gameObject.GetComponent<BuyButton>() != null)
                {
                    hit.collider.gameObject.GetComponent<BuyButton>().OnClicked();
                }
            }
        }
        else
        {
            if(currentHover == null)
            {
                return;
            }

            if (currentHover.GetComponent<GoogleButton>() != null)
            {
                currentHover.GetComponent<GoogleButton>().OnEndHover();
            }
            if (currentHover.GetComponent<DishData>() != null)
            {
                currentHover.GetComponent<DishData>().OnEndHover();
            }
            if (currentHover.GetComponent<BuyButton>() != null)
            {
                currentHover.GetComponent<BuyButton>().OnEndHover();
            }
            currentHover = null;
        }
       
    }
}