using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[ElementPresenter("dispenser", "Presenters/Dispenser/Dispenser XR", false)]
public class DispenserElementPresenterXR : DispenserElementPresenter
{
    [SerializeField] protected ScrollGestureZone GestureInput;
    
    public override async Task Initialize()
    {
        await base.Initialize();

        GestureInput.OnUserInput += GestureInput_OnUserInput;
    }

    private void OnDestroy()
    {
        GestureInput.OnUserInput -= GestureInput_OnUserInput;
    }

    private void GestureInput_OnUserInput()
    {
        FireOnUserInput();
    }

    public int GetNextItemId()
    {
        //if (sync != null) sync.RequestOwnership(); //TODO: remove this once the refactor is complete
        return ItemCounter++;
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
