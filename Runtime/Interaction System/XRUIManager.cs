using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRUIManager : IUIManager
{
    public void MakeGrabbalbe(GameObject Item)
    {
        var body = Item.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.drag = 0.2f;
        body.angularDrag = 0.2f;
        //checkPhysicsMaterials(Item); //TODO: Remove during the great package refactor

        Item.AddComponent<Grabbable>();
        Item.AddComponent<DistanceGrabbable>();
    }
}
