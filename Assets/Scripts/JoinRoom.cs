using UnityEngine;
using System.Collections;

public class JoinRoom : ConnectAndJoinRandom {

    public GameObject Prefab;


    public override void OnJoinedRoom(){

        Debug.Log("# of players: "+PhotonNetwork.countOfPlayers);
        GameObject cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 1, -5), Quaternion.identity, 0);

    }

}
