using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPointer : MonoBehaviour, IUIPointer
{
    [SerializeField] private Hand _PrimaryHand;
    [SerializeField] private LayerMask Layers;
    [SerializeField] private float MaxRayDistance = 10;
    [SerializeField] private LineRenderer Line;
    [SerializeField] private Transform HitPointIndicator;

    private IHandlePointerEvent hoverItem;
    private IHandlePointerEvent currentHoverItem;
    private IHandlePointerEvent currentTriggerItem;
    private IHandlePointerEvent currentGripItem;
    private RaycastHit rayInfo;

    public Hand PrimaryHand => _PrimaryHand;

    private void OnEnable()
    {
        PrimaryHand.OnTriggerGrab += PrimaryHand_OnTriggerGrab;
        PrimaryHand.OnTriggerRelease += PrimaryHand_OnTriggerRelease;
        PrimaryHand.OnSqueezed += PrimaryHand_OnSqueezed;
        PrimaryHand.OnUnsqueezed += PrimaryHand_OnUnsqueezed;
    }


    private void OnDisable()
    {
        PrimaryHand.OnTriggerGrab -= PrimaryHand_OnTriggerGrab;
        PrimaryHand.OnTriggerRelease -= PrimaryHand_OnTriggerRelease;
        PrimaryHand.OnSqueezed -= PrimaryHand_OnSqueezed;
        PrimaryHand.OnUnsqueezed -= PrimaryHand_OnUnsqueezed;
    }

    private void Update()
    {
        if(Physics.Raycast(new Ray(transform.position, transform.forward), out rayInfo, MaxRayDistance, Layers))
        {
            PrimaryHand.AllowGrabbing = false;

            if (rayInfo.collider.gameObject.TryGetComponent<IHandlePointerEvent>(out hoverItem))
            {
                if (hoverItem != currentHoverItem)
                {
                    if (currentHoverItem != null)
                    {
                        notifyEndHover();
                        currentHoverItem = null;
                    }

                    currentHoverItem = hoverItem;
                    notifyHoverStart(rayInfo);
                }
            }
            else
            {
                currentHoverItem = null;
            }
        }
        else
        {
            PrimaryHand.AllowGrabbing = true;

            if (currentHoverItem != null)
            {
                notifyEndHover();
                currentHoverItem = null;
            }
        }

        updateVisuals(rayInfo);
    }

    private void notifyHoverStart(RaycastHit rayInfo)
    {
        try
        {
            if(currentHoverItem != null) currentHoverItem.OnHoverStart(this, rayInfo);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error with UIPoint OnHoverStart: " + ex.Message);
        }
    }

    private void notifyEndHover()
    {
        try
        {
            if(currentHoverItem != null) currentHoverItem.OnHoverEnd(this);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error with UIPointer OnHoverEnd: " + ex.Message);
        }
    }

    private void updateVisuals(RaycastHit hitInfo)
    {
        if (hitInfo.collider != null)
        {
            Line.positionCount = 2;
            Line.SetPositions(new Vector3[] { transform.position, hitInfo.point });
            Line.enabled = true;

            HitPointIndicator.position = hitInfo.point;
            HitPointIndicator.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
            HitPointIndicator.gameObject.SetActive(true);
        }
        else
        {
            Line.enabled = false;
            HitPointIndicator.gameObject.SetActive(false);
        }
    }

    private void PrimaryHand_OnSqueezed(Hand hand, Grabbable grabbable) //Trigger button
    {
        if(currentHoverItem != null)
        {
            currentTriggerItem = currentHoverItem;
            currentTriggerItem.OnTriggerStart(this, rayInfo);
        }
    }

    private void PrimaryHand_OnUnsqueezed(Hand hand, Grabbable grabbable)
    {
        if (currentTriggerItem != null)
        {
            currentTriggerItem.OnTriggerEnd(this);
            currentTriggerItem = null;
        }
    }

    private void PrimaryHand_OnTriggerGrab(Hand hand, Grabbable grabbable) //Grip button
    {
        if(currentHoverItem != null)
        {
            currentGripItem = currentHoverItem;
            currentGripItem.OnGripStart(this, rayInfo);
        }
    }

    private void PrimaryHand_OnTriggerRelease(Hand hand, Grabbable grabbable)
    {
        if(currentGripItem != null)
        {
            currentGripItem.OnGripEnd(this);
            currentGripItem = null;
        }
    }
}
