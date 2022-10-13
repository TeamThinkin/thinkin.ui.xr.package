using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    [SerializeField] private LayoutContainer MenuContentContainer;
    [SerializeField] private LayoutContainer MenuScrollArea;
    [SerializeField] private TMPro.TMP_Text BuildLabel;

    //TODO: commented out during the Package refactor
    //private TabletNetworkSync networkSync;

    private void Start()
    {
        createNetworkSync();
        MenuScrollArea.ExecuteLayout();

        AppSceneManager.OnEnvironmentUnloaded += AppSceneManager_OnEnvironmentUnloaded;

        //BuildLabel.text = Application.version + ", " + GeneratedInfo.BundleVersionCode;//TODO: commented out during the Package refactor
    }

    private void OnDestroy()
    {
        //TODO: commented out during the Package refactor
        //if (networkSync != null)
        //{
        //    Normal.Realtime.Realtime.Destroy(networkSync.gameObject);
        //}

        AppSceneManager.OnEnvironmentUnloaded -= AppSceneManager_OnEnvironmentUnloaded;
    }

    private void createNetworkSync()
    {
        //TODO: commented out during the Package refactor
        //if (!TelepresenceRoomManager.Instance.IsConnected) return;

        //networkSync = Normal.Realtime.Realtime.Instantiate("Tablet (Remote)", Normal.Realtime.Realtime.InstantiateOptions.defaults).GetComponent<TabletNetworkSync>();
        //networkSync.SetSource(this);
    }

    private void AppSceneManager_OnEnvironmentUnloaded()
    {
        Destroy(this.gameObject);
    }

}
