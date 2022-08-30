using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class ConfigPanel : TabPanel
{
    [SerializeField] private TMPro.TMP_Text CurrentUserDisplayNameLabel;
    [SerializeField] private TMPro.TMP_Text ConnectedUsersLabel;

    public void LogoutButtonPressed()
    {
        //TODO: commented out during the Package refactor
        //Debug.Log("Logging out...");
        //DeviceRegistrationController.Logout();
        //updateCurrentUserInfo();
        //Debug.Log("Logged out");
    }

    private void OnEnable()
    {
        //TODO: commented out during the Package refactor
        //TelepresenceRoomManager.Instance.OnUserListChanged += Instance_OnUserListChanged;
        //updateCurrentUserInfo();
        //updateConnectedUsers();
    }

    private void OnDisable()
    {
        //TODO: commented out during the Package refactor
        //TelepresenceRoomManager.Instance.OnUserListChanged -= Instance_OnUserListChanged;
    }

    private void Instance_OnUserListChanged()
    {
        updateConnectedUsers();
    }

    private void updateCurrentUserInfo()
    {
        //TODO: commented out during the Package refactor
        //if(UserInfo.CurrentUser != null && UserInfo.CurrentUser != UserInfo.UnknownUser)
        //{
        //    CurrentUserDisplayNameLabel.text = UserInfo.CurrentUser.DisplayName;
        //}
        //else
        //{
        //    CurrentUserDisplayNameLabel.text = "<Logged Out>";
        //}
    }

    private void updateConnectedUsers()
    {
        //ConnectedUsersLabel.text = string.Join('\n', TelepresenceRoomManager.Instance.ConnectedUsers.Select(i => i.displayName).ToArray());//TODO: commented out during the Package refactor
    }
}
