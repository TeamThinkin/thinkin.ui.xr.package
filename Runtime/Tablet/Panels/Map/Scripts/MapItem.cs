using Autohand;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapItem : HandTouchEvent, IHandlePointerEvent
{
    //[SerializeField] private ContentSymbol _symbol;//TODO: commented out during the Package refactor
    [SerializeField] private VoumeConstraintedPointerGrabbable Grabbable;

    //TODO: commented out during the Package refactor
    //public DestinationLinkContentItemDto Dto { get; private set; }
    //public ContentSymbol Symbol => _symbol;

    private bool isEditable;

    //TODO: commented out during the Package refactor
    //public void SetDto(DestinationLinkContentItemDto Dto)
    //{
    //    this.Dto = Dto;
    //    Symbol.SetDto(Dto);
    //}

    public void ToggleEditable(bool IsEditable)
    {
        isEditable = IsEditable;
        Grabbable.enabled = IsEditable;
    }

    private async void onInteraction()
    {
        if (isEditable) return;

        //await DestinationPresenter.Instance.DisplayUrl(Dto.Url);//TODO: commented out during the Package refactor
    }

    protected override void OnTouch(Hand hand, Collision collision)
    {
        base.OnTouch(hand, collision);
        return;

        if (!collision.InvolvesPrimaryFingerTip()) return; //Only accept input from pointer finger tips to hopefully filter out accidental touches

        onInteraction();
    }

    public void OnTriggerStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        onInteraction();
    }


    public void OnGripStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        if (!isEditable) return;

        Grabbable.OnGripStart(Sender, RayInfo);
    }

    public void OnGripEnd(IUIPointer Sender)
    {
        if (!isEditable) return;

        Grabbable.OnGripEnd(Sender);
    }


    #region -- Unused Pointer Events --
    public void OnTriggerEnd(IUIPointer Sender)
    {
    }

    public void OnHoverStart(IUIPointer Sender, RaycastHit RayInfo)
    {
    }

    public void OnHoverEnd(IUIPointer Sender)
    {
    }

    #endregion
}
