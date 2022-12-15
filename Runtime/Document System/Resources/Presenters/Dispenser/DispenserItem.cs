using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserItem : MonoBehaviour, IHandlePointerEvent
{
    public DispenserElementPresenterXR ParentDispenser;

    private DispenserElementPresenter.ItemInfo itemInfo;
    private AudioSource audioSource;

    private bool isGrabbed;
    private bool isLinked;
    private Vector3 initialGrabPosition;
    private const float pluckDistanceThreshold = 0.2f;
    private const float pluckDistanceThresholdSquared = pluckDistanceThreshold * pluckDistanceThreshold;

    private void Awake()
    {
        //this.gameObject.layer = LayerMask.NameToLayer("UI");
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SetItemInfo(DispenserElementPresenter.ItemInfo ItemInfo)
    {
        this.itemInfo = ItemInfo;

        var grabbable = GetComponent<IGrabbable>();
        grabbable.OnBeforeGrab += Grabbable_OnBeforeGrab;
        grabbable.OnRelease += Grabbable_OnRelease;
        isLinked = true;
    }

    private void Grabbable_OnBeforeGrab(IGrabber Grabber, IGrabbable Grabbable)
    {
        audioSource.PlayOneShot(ParentDispenser.ItemGrabSound, 1f);
        itemInfo.IsDisconnected = true;
        itemInfo.Body.isKinematic = false;
        isGrabbed = true;
        initialGrabPosition = transform.position;
        ParentDispenser.DisableGestureZone();
    }

    private void Grabbable_OnRelease(IGrabber Grabber, IGrabbable Grabbable)
    {
        isGrabbed = false;
        ParentDispenser.EnableGestureZone();

        if (isLinked)
        {
            audioSource.PlayOneShot(ParentDispenser.ItemRestoreSound);
            itemInfo.IsDisconnected = false;
            itemInfo.Body.isKinematic = true;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnGripStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    //    return; //TODO: temp thing during pancake refactor
    //    var clone = Instantiate(itemInfo.Prefab);
    //    clone.name = ParentDispenser.gameObject.name + ": " + gameObject.name + ParentDispenser.GetNextItemId();
    //    clone.transform.localScale = 0.1f * Vector3.one;
    //    clone.transform.position = Sender.transform.position + Sender.transform.right * -0.1f;

    //    ParentDispenser.FireOnItemDispensed(clone, itemInfo);

    //    var hand = Sender.transform.gameObject.GetComponentInParent<Hand>();
    //    AppControllerBase.Instance.UIManager.MakeGrabbable(clone);
    //    StartCoroutine(attachToHand(hand, clone));
    }

    private void Update()
    {
        if(isGrabbed && isLinked && (transform.position - initialGrabPosition).sqrMagnitude > pluckDistanceThresholdSquared)
        {
            onItemPlucked();
        }
    }

    private void onItemPlucked()
    {
        audioSource.PlayOneShot(ParentDispenser.ItemReleaseSound);
        isLinked = false;
        itemInfo.Line.gameObject.SetActive(false);

        transform.SetParent(ParentDispenser.GetRootParent().transform);
        gameObject.name = ParentDispenser.gameObject.name + ": " + itemInfo.Prefab.name + " " + ParentDispenser.GetNextItemId(); //NOTE: is important that the name of potentially networked items be unique
        ParentDispenser.ReplaceItem(itemInfo);
        ParentDispenser.FireOnItemDispensed(this.gameObject, itemInfo);
    }

    //private IEnumerator attachToHand(Hand hand, GameObject clone)
    //{
    //    yield return new WaitForEndOfFrame(); //The grabbable seems to need some things to be setup in the first frame before the TryGrab can succeed

    //    var grabbable = clone.GetComponent<Grabbable>();
    //    hand.AllowGrabbing = true;
    //    hand.TryGrab(grabbable);
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
