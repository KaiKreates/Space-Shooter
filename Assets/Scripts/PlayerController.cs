using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Sprite enemyRocket;

    private PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            GetComponent<SpriteRenderer>().sprite = enemyRocket;
        }
    }
    private void Update()
    {
        //Shoot
        if (Input.GetKeyDown(KeyCode.Space) && view.IsMine)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            //Movement
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 2);
            }
            transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * 5));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Gameover
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
}
