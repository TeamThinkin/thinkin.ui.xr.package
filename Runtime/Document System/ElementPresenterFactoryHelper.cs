using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ElementPresenterFactoryHelper
{
    public static void RegisterAdditionalTypes()
    {
        ElementPresenterFactory.DiscoverTypes(Assembly.GetExecutingAssembly());
    }
}
