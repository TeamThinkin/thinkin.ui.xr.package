using Autohand;
using System.Collections;
using UnityEngine;

public class XRGrabbable : MonoBehaviour, IGrabbable
{
    public event IGrabbable.GrabEventDelegate OnBeforeGrab;
    public event IGrabbable.GrabEventDelegate OnGrab;
    public event IGrabbable.GrabEventDelegate OnRelease;

    private Grabbable grabbable;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        grabbable.onGrab.AddListener(Grabbable_OnGrab);
        grabbable.onRelease.AddListener(Grabbable_OnRelease);
    }

    private void Grabbable_OnGrab(Hand hand, Grabbable grabbable)
    {
        var grabber = hand.GetComponent<IGrabber>();
        this.OnGrab?.Invoke(grabber, this);
    }

    private void Grabbable_OnRelease(Hand hand, Grabbable grabbable)
    {
        var grabber = hand.GetComponent<IGrabber>();
        this.OnRelease?.Invoke(grabber, this);
    }
}