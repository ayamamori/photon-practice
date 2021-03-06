﻿using UnityEngine;

public class MouseDrag : MonoBehaviour
{
	private PhotonView pView;
	private Rigidbody rBody;
	private int i=0;

	void Awake(){
		pView = GetComponent<PhotonView> ();
		rBody = GetComponent<Rigidbody> ();
		rBody.isKinematic = !pView.isMine;
	}

	void OnMouseDown(){
		if (!pView.isMine) return;

		rBody.isKinematic = true;

		Room room = PhotonNetwork.room;
		room.SetCustomProperties (new ExitGames.Client.Photon.Hashtable{{"a", i++}});

	}
	void OnMouseUp(){
		if (!pView.isMine) return;

		rBody.isKinematic = false;
	}

	void OnMouseDrag()
	{
		if (!pView.isMine) return;

		Vector3 objectPointInScreen
		= Camera.main.WorldToScreenPoint(this.transform.position);

		Vector3 mousePointInScreen
		= new Vector3(Input.mousePosition.x,
			Input.mousePosition.y,
			objectPointInScreen.z);

		Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
		mousePointInWorld.z = this.transform.position.z;
		this.transform.position = mousePointInWorld;
	}
}