using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentListItem : MonoBehaviour, IHandlePointerEvent
{
    [SerializeField] private GameObject SelectedIndicator;
    [SerializeField] private GameObject HoverIndicator;
    //[SerializeField] private ContentSymbol Symbol; //TODO: commented out during the Package refactor

    //public CollectionContentItemDto Dto { get; private set; } //TODO: commented out during the Package refactor

    public bool IsItemSelected
    {
        get { return SelectedIndicator.activeSelf; }
        set { SelectedIndicator.SetActive(value); }
    }

    public bool IsItemHovered
    {
        get { return HoverIndicator.activeSelf; }
        private set { HoverIndicator.SetActive(value); }
    }

    private void Start()
    {
        IsItemSelected = false;
        IsItemHovered = false;
    }

    //TODO: commented out during the Package refactor
    //public void SetDto(CollectionContentItemDto Dto)
    //{
    //    this.Dto = Dto;
    //    Symbol.SetDto(Dto);
    //}

    public void OnHoverStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        IsItemHovered = true;
    }

    public void OnHoverEnd(IUIPointer Sender)
    {
        IsItemHovered = false;
    }

    public void OnGripStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        Debug.Log("Content List Item: Grip Start");
    }

    public void OnGripEnd(IUIPointer Sender)
    {
    }

    public void OnTriggerStart(IUIPointer Sender, RaycastHit RayInfo)
    {
        Debug.Log("Content List Item: Trigger Press");
    }

    public void OnTriggerEnd(IUIPointer Sender)
    {
    }
}
