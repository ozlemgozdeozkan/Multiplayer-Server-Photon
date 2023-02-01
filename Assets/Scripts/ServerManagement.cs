using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Olumlu Callback fonksiyonlar
    public override void OnConnectedToMaster()//Sunucuya ba�lan�nca �al���r
    {
        Debug.Log("Sunucuya ba�lan�ld�.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // Lobiye ba�land���nda �al���r
    {
        Debug.Log("Lobiye ba�lan�ld�.");
        PhotonNetwork.JoinOrCreateRoom("G�zde", new RoomOptions {MaxPlayers = 2, IsOpen =true, IsVisible =true }, TypedLobby.Default);
        /*RoomOptions 
         Maksimum oyuncu sya�s�{MaxPlayers = 2,
         Oda a��k m� IsOpen = true,
         Oda g�r�n�r m� IsVisible =true }
         */
     }
    public override void OnJoinedRoom() // Odaya ba�land���nda �al���r
    {
        Debug.Log("Odaya ba�lan�ld�.");
        GameObject myObject = PhotonNetwork.Instantiate("Player1", Vector3.zero, Quaternion.identity, 0, null);
        myObject.GetComponent<PhotonView>().Owner.NickName = "G�zde";
    }
    public override void OnLeftRoom() // Lobiden ��k�nca �al���r
    {
        Debug.Log("Lobiden ��k�ld�.");
    }
    //Olumsuz Callback Fonksiyonlar
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Odaya giri� yap�lamad�");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Random bir odaya girilemedi");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Oda olu�turulurken hata olu�tu.");
    }


}
