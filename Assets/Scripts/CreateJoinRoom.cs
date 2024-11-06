using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomInput;
    public TMP_InputField joinRoomInput;
    public TextMeshProUGUI waitText;
    public GameObject WaitingScreen, CreateJoinScreen;

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createRoomInput.text, options);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        WaitingScreen.SetActive(true);
        CreateJoinScreen.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            waitText.text = createRoomInput.text + "\n Waiting for other player to join";
        }
        else
        {
            waitText.text = "Starting the game...";
            StartCoroutine(LoadGame());
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room");
        Debug.Log(PhotonNetwork.CurrentRoom.Players);
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                waitText.text = "Starting the game...";
                StartCoroutine(LoadGame());
            }
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel("Game");

    }
}
