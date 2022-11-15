using Autohand;
using System.Collections;
using System.Reflection;
using UnityEngine;

public static class XrUIModule
{
    public static void Initialize()
    {
        ElementPresenterFactory.DiscoverTypes(Assembly.GetExecutingAssembly());
        AppControllerBase.Instance.UIManager.OnMakeGrabbable += UIManager_OnMakeGrabbable;
    }

    private static void UIManager_OnMakeGrabbable(GameObject Item)
    {
        Rigidbody body = Item.GetComponent<Rigidbody>();
        if (body == null)
        {
            body = Item.AddComponent<Rigidbody>();
            body.useGravity = false;
            body.drag = 0.2f;
            body.angularDrag = 0.2f;
            //checkPhysicsMaterials(Item); //TODO: Would be nice to assign a default physics material so things have some bounce
        }

        Grabbable grabbable = Item.GetComponent<Grabbable>();
        if (grabbable == null)
        {
            grabbable = Item.AddComponent<Grabbable>();
            grabbable.parentOnGrab = false;

            Item.AddComponent<DistanceGrabbable>();
            Item.AddComponent<XRGrabbable>();
        }
    }
}
