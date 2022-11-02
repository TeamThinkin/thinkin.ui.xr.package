using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class UIPointerProviderXR : MonoBehaviour, IUIPointerProvider
{
    public event System.Action PrimaryButtonStart;
    public event System.Action PrimaryButtonEnd;
    public event System.Action SecondaryButtonStart;
    public event System.Action SecondaryButtonEnd;

    [SerializeField] private Hand _PrimaryHand;
    public Hand PrimaryHand => _PrimaryHand;

    private void Awake()
    {
        GetComponent<IUIPointer>().SetProvider(this);
    }

    private void OnEnable()
    {
        _PrimaryHand.OnTriggerGrab += PrimaryHand_OnTriggerGrab;
        _PrimaryHand.OnTriggerRelease += PrimaryHand_OnTriggerRelease;
        _PrimaryHand.OnSqueezed += PrimaryHand_OnSqueezed;
        _PrimaryHand.OnUnsqueezed += PrimaryHand_OnUnsqueezed;
    }


    private void OnDisable()
    {
        _PrimaryHand.OnTriggerGrab -= PrimaryHand_OnTriggerGrab;
        _PrimaryHand.OnTriggerRelease -= PrimaryHand_OnTriggerRelease;
        _PrimaryHand.OnSqueezed -= PrimaryHand_OnSqueezed;
        _PrimaryHand.OnUnsqueezed -= PrimaryHand_OnUnsqueezed;
    }

    public Ray GetRay()
    {
        return new Ray(transform.position, transform.forward);
    }


    private void PrimaryHand_OnSqueezed(Hand hand, Grabbable grabbable) //Trigger button
    {
        PrimaryButtonStart?.Invoke();
    }

    private void PrimaryHand_OnUnsqueezed(Hand hand, Grabbable grabbable)
    {
        PrimaryButtonEnd?.Invoke();
    }

    private void PrimaryHand_OnTriggerGrab(Hand hand, Grabbable grabbable) //Grip button
    {
        SecondaryButtonStart?.Invoke();
    }

    private void PrimaryHand_OnTriggerRelease(Hand hand, Grabbable grabbable)
    {
        SecondaryButtonEnd?.Invoke();
    }
}
