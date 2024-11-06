using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float maxX;
    public float maxY;

    private void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
