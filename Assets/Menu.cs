using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Menu : MonoBehaviour
{
    private static Menu _instance;
    public bool DidPlayerNameLobby = false;
    public string LobbyName = "";
    public List<CSteamID> CurrentLobbies;
    public static Menu Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            CurrentLobbies = new List<CSteamID>();
        }
        else
        {
            Debug.LogError("Two Main Menus are not allowed to exist!");
            Destroy(this.gameObject);
        }
    }
    public void CreateLobby()
    {
        ELobbyType newLobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
        DidPlayerNameLobby = true;
        LobbyName = "Test lobby";
        /*
        if (friendsOnlyToggle.isOn)
        {
            Debug.Log("CreateNewLobby: friendsOnlyToggle is on. Making lobby friends only.");
            newLobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
        }
        else
        {
            Debug.Log("CreateNewLobby: friendsOnlyToggle is OFF. Making lobby public.");
            newLobbyType = ELobbyType.k_ELobbyTypePublic;
        }

        if (!string.IsNullOrEmpty(lobbyNameInputField.text))
        {
            Debug.Log("CreateNewLobby: player created a lobby name of: " + lobbyNameInputField.text);
            didPlayerNameTheLobby = true;
            lobbyName = lobbyNameInputField.text;
        }
        */
        SteamLobby.instance.CreateNewLobby(newLobbyType);
    }

    public void DestroyOldLobbyListItems()
    {

    }

    public void DisplayLobbies(List<CSteamID> lobbyIDS, LobbyDataUpdate_t result)
    {

    }
}
