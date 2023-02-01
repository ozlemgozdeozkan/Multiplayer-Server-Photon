using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    PhotonView pv;
    int health = 100;

    public GameObject[] locations;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        
    }
    void Start()
    {
        
        if (pv.IsMine)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            transform.position = locations[0].transform.position;
        }
        else
        {
            transform.position = locations[1].transform.position;
        }

    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 100f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 100f;
        transform.Translate(x, 0, z);

    }
    void Update()
    {
        if (pv.IsMine) 
            Move();
            Jump();
            Fire();
        }
                 
        
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 10f, 0);//10birim z�pla

        }
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("Hit", RpcTarget.All, 20);
            }
        }
    }
    [PunRPC] 
    void Hit(int hitPower)
    {
        health -= hitPower;
        Debug.Log("Kalan Sa�l�k: " + health);
        if(health<= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
      
    }

    
}
