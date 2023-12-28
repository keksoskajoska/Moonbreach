using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyListItem : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text LobbyName;

    public CSteamID lobbyID;
    public string lobbyName;
    public int playerCount;
    public int maxPlayers;

    public void SetValues()
    {
        LobbyName.text = lobbyName;
    }
}
