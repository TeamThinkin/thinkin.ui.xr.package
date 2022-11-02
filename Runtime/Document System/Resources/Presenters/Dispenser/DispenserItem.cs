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
        clone.name = ParentDispenser.gameObject.name + ": " + gameObject.name + ParentDispenser.GetNextItemId();
        clone.transform.localScale = 0.1f * Vector3.one;
        clone.transform.position = Sender.transform.position + Sender.transform.right * -0.1f;

        ParentDispenser.FireOnItemDispensed(clone, itemInfo);

        var hand = Sender.transform.gameObject.GetComponentInParent<Hand>();
        AppControllerBase.Instance.UIManager.MakeGrabbable(clone);
        StartCoroutine(attachToHand(hand, clone));
    }

    private IEnumerator attachToHand(Hand hand, GameObject clone)
    {
        yield return new WaitForEndOfFrame(); //The grabbable seems to need some things to be setup in the first frame before the TryGrab can succeed

        var grabbable = clone.GetComponent<Grabbable>();
        hand.AllowGrabbing = true;
        hand.TryGrab(grabbable);
    }

    //private void checkPhysicsMaterials(GameObject item)
    //{
    //    var colliders = item.GetComponentsInChildren<Collider>();
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.sharedMaterial == null) collider.sharedMaterial = ParentDispenser.DefaultPhysicsMaterial;
    //    }
    //}

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
