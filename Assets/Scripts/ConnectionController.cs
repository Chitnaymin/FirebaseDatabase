using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
	public bool isOnline;
	public bool isOffline;
	private void Start() {
		isOffline = false;
		isOnline = false;
	}
	public void Online() {
		isOffline = false;
		isOnline = true;
	}
	public void Offline() {
		isOnline = false;
		isOffline = true;
	}


}
