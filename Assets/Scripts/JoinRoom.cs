using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : ConnectAndJoinRandom {

    public GameObject Prefab;
    public Text ClientNumberText;


    public override void OnJoinedRoom(){

        Debug.Log("# of players: "+PhotonNetwork.countOfPlayers);
        GameObject cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 2, -5), Quaternion.identity, 0);
        ClientNumberText.text = "You're No. "+PhotonNetwork.player.ID;
    }

}
