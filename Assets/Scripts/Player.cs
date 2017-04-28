using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	private Transform cannon;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;


	void Awake(){
		cannon = this.gameObject.transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		cannon.transform.Rotate (y, 0, -x);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 200;

		NetworkServer.Spawn (bullet);

		Destroy (bullet, 2);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}
