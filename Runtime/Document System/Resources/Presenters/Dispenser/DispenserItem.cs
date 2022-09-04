using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserItem : MonoBehaviour, IHandlePointerEvent
{
    public DispenserElementPresenterXR ParentDispenser;

    private DispenserElementPresenter.ItemInfo itemInfo;

    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("UI");
    }

    public void SetItemInfo(DispenserElementPresenter.ItemInfo ItemInfo)
    {
        this.itemInfo = ItemInfo;
    }

    public void OnGripStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        var clone = Instantiate(itemInfo.Prefab, ParentDispenser.SceneChildrenContainer.transform);
        clone.name = gameObject.name + " Clone " + ParentDispenser.GetNextItemId();
        clone.transform.localScale = 0.1f * Vector3.one;
        clone.transform.position = Sender.transform.position + Sender.transform.right * -0.1f;

        //NetworkItemSync.MakeGrabbable(clone);
        //var networkSync = NetworkItemSync.FindOrCreate(clone, itemInfo.AssetSourceUrl);

        Debug.Log("Looking for Hand from uipointer", Sender.transform.gameObject);
        var hand = Sender.transform.gameObject.GetComponentInParent<Hand>();
        makeGrabbable(clone);
        StartCoroutine(attachToHand(hand, clone));
    }


    private void makeGrabbable(GameObject item)
    {
        var body = item.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.drag = 0.2f;
        body.angularDrag = 0.2f;
        checkPhysicsMaterials(item);

        item.AddComponent<Grabbable>();
        item.AddComponent<DistanceGrabbable>();
    }

    private IEnumerator attachToHand(Hand hand, GameObject clone)
    {
        yield return new WaitForEndOfFrame(); //The grabbable seems to need some things to be setup in the first frame before the TryGrab can succeed

        var grabbable = clone.GetComponent<Grabbable>();
        hand.AllowGrabbing = true;
        hand.TryGrab(grabbable);
    }

    private void checkPhysicsMaterials(GameObject item)
    {
        var colliders = item.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            if (collider.sharedMaterial == null) collider.sharedMaterial = ParentDispenser.DefaultPhysicsMaterial;
        }
    }

    #region -- Unused IHandlePointerEvent's --
    public void OnGripEnd(IUIPointer Sender)
    {
    }

    public void OnHoverStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    }

    public void OnHoverEnd(IUIPointer Sender)
    {
    }

    public void OnTriggerStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    }

    public void OnTriggerEnd(IUIPointer Sender)
    {
    }
    #endregion


}
