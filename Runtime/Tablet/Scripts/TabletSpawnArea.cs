using Autohand;
using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletSpawnArea : HandTriggerAreaEvents
{
    [SerializeField] private GameObject TabletPrefab;

    private Tablet hoverTablet;

    public override void Enter(Hand hand)
    {
        base.Enter(hand);
        if (!this.enabled) return;

        var heldItem = hand.GetHeld();
        if(heldItem != null) hoverTablet = heldItem.GetComponent<Tablet>();
    }

    public override void Exit(Hand hand)
    {
        base.Exit(hand);
        if (!this.enabled) return;

        hoverTablet = null;
    }

    public override void Release(Hand hand)
    {
        base.Release(hand);
        if (!this.enabled) return;

        if (hoverTablet != null)
        {
            Destroy(hoverTablet.gameObject);
        }
        hoverTablet = null;
    }

    public override void Grab(Hand hand)
    {
        base.Grab(hand);
        if (!this.enabled) return;

        var tablet = Instantiate(TabletPrefab);
        tablet.transform.position = hand.transform.position;
        tablet.transform.rotation = hand.transform.rotation;

        StartCoroutine(attachToHand(hand, tablet));
    }

    private IEnumerator attachToHand(Hand hand, GameObject tablet)
    {
        yield return new WaitForEndOfFrame(); //The grabbable seems to need some things to be setup in the first frame before the TryGrab can succeed

        var tabletGrabbable = tablet.GetComponent<Grabbable>();
        hand.TryGrab(tabletGrabbable);
        hoverTablet = tablet.GetComponent<Tablet>();
    }
}