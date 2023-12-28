using Steamworks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject HostMenu;
    public GameObject JoinMenu;
    public Toggle PrivateToggle;
    public TMP_InputField LobbyNameInput;
    public TMP_InputField LobbySearchInput;
    public GameObject LobbyListContent;

    private static Menu _instance;
    public bool DidPlayerNameLobby = false;
    public bool DidPlayerSearch = false;
    public string LobbyName = "";
    public List<GameObject> CurrentLobbies;
    public GameObject ListItemPrefab;
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

            CurrentLobbies = new List<GameObject>();
        }
        else
        {
            Debug.LogError("Two Main Menus are not allowed to exist!");
            Destroy(this.gameObject);
        }
    }
    public void CreateLobby()
    {
        ELobbyType newLobbyType;
        if (PrivateToggle.isOn)
        {
            Debug.Log("CreateNewLobby: friendsOnlyToggle is on. Making lobby friends only.");
            newLobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
        }
        else
        {
            Debug.Log("CreateNewLobby: friendsOnlyToggle is OFF. Making lobby public.");
            newLobbyType = ELobbyType.k_ELobbyTypePublic;
        }

        if (!string.IsNullOrEmpty(LobbyNameInput.text))
        {
            Debug.Log("CreateNewLobby: player created a lobby name of: " + LobbyNameInput.text);
            LobbyName = LobbyNameInput.text;
            DidPlayerNameLobby = true;
        }

        SteamLobby.instance.CreateNewLobby(newLobbyType);
    }

    public void DestroyOldLobbyListItems()
    {
        Debug.Log("DestroyOldLobbyListItems");
        foreach (GameObject lobbyListItem in CurrentLobbies)
        {
            GameObject lobbyListItemToDestroy = lobbyListItem;
            Destroy(lobbyListItemToDestroy);
            lobbyListItemToDestroy = null;
        }
        CurrentLobbies.Clear();
    }

    public void DisplayLobbies(List<CSteamID> lobbyIDS, LobbyDataUpdate_t result)
    {
        for (int i = 0; i < lobbyIDS.Count; i++)
        {
            if (lobbyIDS[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                Debug.Log("Lobby " + i + " :: " + SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name") + " number of players: " + SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbyIDS[i].m_SteamID).ToString() + " max players: " + SteamMatchmaking.GetLobbyMemberLimit((CSteamID)lobbyIDS[i].m_SteamID).ToString());

                if (DidPlayerSearch)
                {
                    Debug.Log("OnGetLobbyInfo: Player searched for lobbies");
                    if (SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name").ToLower().Contains(LobbySearchInput.text.ToLower()))
                    {
                        GameObject newLobbyListItem = Instantiate(ListItemPrefab) as GameObject;
                        LobbyListItem newLobbyListItemScript = newLobbyListItem.GetComponent<LobbyListItem>();

                        newLobbyListItemScript.lobbyID = (CSteamID)lobbyIDS[i].m_SteamID;
                        newLobbyListItemScript.lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name");
                        newLobbyListItemScript.playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbyIDS[i].m_SteamID);
                        newLobbyListItemScript.maxPlayers = SteamMatchmaking.GetLobbyMemberLimit((CSteamID)lobbyIDS[i].m_SteamID);
                        newLobbyListItemScript.SetValues();


                        newLobbyListItem.transform.SetParent(LobbyListContent.transform);
                        newLobbyListItem.transform.localScale = Vector3.one;

                        CurrentLobbies.Add(newLobbyListItem);
                    }
                }
                else
                {
                    Debug.Log("OnGetLobbyInfo: Player DID NOT search for lobbies");
                    GameObject newLobbyListItem = Instantiate(ListItemPrefab) as GameObject;
                    LobbyListItem newLobbyListItemScript = newLobbyListItem.GetComponent<LobbyListItem>();

                    newLobbyListItemScript.lobbyID = (CSteamID)lobbyIDS[i].m_SteamID;
                    newLobbyListItemScript.lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name");
                    newLobbyListItemScript.playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbyIDS[i].m_SteamID);
                    newLobbyListItemScript.maxPlayers = SteamMatchmaking.GetLobbyMemberLimit((CSteamID)lobbyIDS[i].m_SteamID);
                    newLobbyListItemScript.SetValues();


                    newLobbyListItem.transform.SetParent(LobbyListContent.transform);
                    newLobbyListItem.transform.localScale = Vector3.one;

                    CurrentLobbies.Add(newLobbyListItem);
                }

                return;
            }
        }
        if (DidPlayerSearch)
        {
            DidPlayerSearch = false;
            LobbySearchInput.text = "";
        }
            
    }

    // We need separate methods for each menu direction in case we want to do some initialization
    // That cant be done in some generic navigation method
    // IMPORTANT:
    // These are referenced by buttons in Unity
    public void ProgressToPlay()
    {
        PlayMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void BackToMain()
    {
        PlayMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ProgressToHost()
    {
        HostMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void BackToPlayFromHost()
    {
        HostMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void ProgressToJoin()
    {
        JoinMenu.SetActive(true);
        PlayMenu.SetActive(false);

        SteamLobby.instance.GetListOfLobbies();
    }

    public void BackToPlayFromJoin()
    {
        JoinMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }
}
