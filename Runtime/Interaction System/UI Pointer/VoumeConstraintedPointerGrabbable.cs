using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoumeConstraintedPointerGrabbable : MonoBehaviour, IHandlePointerEvent
{
    public ConstraintVolume Volume;

    private Vector3 interactorGrabPoint;
    private IUIPointer interactor;

    private void Start()
    {
        if (Volume == null) Volume = GetComponentInParent<ConstraintVolume>();
    }

    public void OnGripStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        Debug.Log("Volume grabbable grip start");

        this.interactor = Sender;
        interactorGrabPoint = interactor.transform.InverseTransformPoint(transform.position);
    }

    public void OnGripEnd(IUIPointer Sender)
    {
        interactor = null;
    }

    private void Update()
    {
        if (interactor != null)
        {
            var position = interactor.transform.TransformPoint(interactorGrabPoint); //world space
            position = Volume.VolumeReference.InverseTransformPoint(position); //volume space
            position.x = Mathf.Clamp(position.x, -0.5f, 0.5f);
            position.y = Mathf.Clamp(position.y, -0.5f, 0.5f);
            position.z = Mathf.Clamp(position.z, -0.5f, 0.5f);
            position = Volume.VolumeReference.TransformPoint(position); //world space
            transform.position = position;
        }
        
    }

    #region -- Unused Pointer Events --
    public void OnHoverEnd(IUIPointer Sender)
    {
    }

    public void OnHoverStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    }

    public void OnTriggerEnd(IUIPointer Sender)
    {
    }

    public void OnTriggerStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    }
    #endregion
}
