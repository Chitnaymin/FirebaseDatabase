using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class User
{
	public int UID;
	public string username;
	public string email;

	public User(int id, string name) {
		this.UID = id;
		this.username = name;
	}

	public User(int id,string username, string email) {
		this.UID = id;
		this.username = username;
		this.email = email;
	}

	public Dictionary<string, Object> ToDictionary() {
		Dictionary<string, Object> result = new Dictionary<string, Object>();
		//result["UID"] = UID;
		//result["username"] = username;

		return result;
	}
}
