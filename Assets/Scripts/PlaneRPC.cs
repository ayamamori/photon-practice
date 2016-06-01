using Photon;
using UnityEngine;

public class PlaneRPC : Photon.PunBehaviour {
    int i = 0;

    void OnMouseDown() {
        photonView.RPC("ClickClickClickClickClick", PhotonTargets.All, i++);
    }

    [PunRPC]
    void ClickClickClickClickClick(int i) {
        Debug.Log(i);
    }
}
