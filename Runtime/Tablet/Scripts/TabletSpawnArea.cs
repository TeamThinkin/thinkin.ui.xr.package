using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletSpawnArea : HandTriggerAreaEvents
{
    [SerializeField] private GameObject TabletPrefab;

    private Tablet hoverTablet;
    private Tablet tabletInstance;

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
            Debug.Log("Destroying tablet in spawn zone");
            //Destroy(hoverTablet.gameObject);
            hoverTablet.gameObject.SetActive(false);
        }
        hoverTablet = null;
    }

    public override void Grab(Hand hand)
    {
        base.Grab(hand);
        if (!this.enabled) return;

        if (tabletInstance == null)
        {
            tabletInstance = Instantiate(TabletPrefab).GetComponent<Tablet>();
            tabletInstance.gameObject.name = "Tablet (" + Time.time.GetHashCode() + ")";
        }
        else tabletInstance.gameObject.SetActive(true);

        tabletInstance.transform.position = hand.transform.position;
        tabletInstance.transform.rotation = hand.transform.rotation;
        tabletInstance.transform.Rotate(hand.transform.forward - hand.transform.up, 180);

        StartCoroutine(attachToHand(hand, tabletInstance.gameObject));
    }

    private IEnumerator attachToHand(Hand hand, GameObject tablet)
    {
        yield return new WaitForEndOfFrame(); //The grabbable seems to need some things to be setup in the first frame before the TryGrab can succeed

        var tabletGrabbable = tablet.GetComponent<Grabbable>();
        tabletGrabbable.body.isKinematic = false;
        tabletGrabbable.wasKinematic = true;
        hand.TryGrab(tabletGrabbable);
        hoverTablet = tablet.GetComponent<Tablet>();
    }
}
