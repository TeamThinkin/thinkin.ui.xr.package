using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ElementPresenter("dispenser", "Presenters/Dispenser/Dispenser XR", false)]
public class DispenserElementPresenterXR : DispenserElementPresenter
{
    [SerializeField] protected ScrollGestureZone GestureInput;
    
    protected int itemCounter;
    
    public int GetNextItemId()
    {
        //if (sync != null) sync.RequestOwnership(); //TODO: remove this once the refactor is complete
        return itemCounter++;
    }

    protected override void onDispenserItemCreate(ItemInfo item)
    {
        base.onDispenserItemCreate(item);

        var dispenserItem = item.Instance.AddComponent<DispenserItem>();
        dispenserItem.SetItemInfo(item);
        dispenserItem.ParentDispenser = this;
    }

    protected override void Update()
    {
        trackGestureInput();
        base.Update();
    }

    private void trackGestureInput()
    {
        Scroll += GestureInput.ScrollValue;
    }
}
