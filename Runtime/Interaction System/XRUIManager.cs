using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRUIManager : IUIManager
{
    public void MakeGrabbable(GameObject Item)
    {
        var body = Item.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.drag = 0.2f;
        body.angularDrag = 0.2f;
        //checkPhysicsMaterials(Item); //TODO: Would be nice to assign a default physics material so things have some bounce

        var grabbable = Item.AddComponent<Grabbable>();
        grabbable.parentOnGrab = false;

        Item.AddComponent<DistanceGrabbable>();
        Item.AddComponent<XRGrabbable>();
    }
}
