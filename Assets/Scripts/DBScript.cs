using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using Newtonsoft.Json;

public class DBScript : MonoBehaviour {
	public string userId;
	public InputField _name;
	public InputField _email;
	public Text txtData;
	private int UID;
	List<User> userList = new List<User>();

	// Start is called before the first frame update
	void Start() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dbtest-7dd25.firebaseio.com/");
	}

	// Update is called once per frame
	void Update() {

	}

	private void WriteNewUser(string userId, string name, string email) {
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		UID = int.Parse(userId);
		User user = new User(UID, name, email);
		string json = JsonUtility.ToJson(user);
		string key = reference.Child("Users").Push().Key;
		reference.Child("Users").Child(key).SetRawJsonValueAsync(json);
	}

	public void SaveDB() {
		if (ConnectionController.Instance().isOnline == true) {
			string name = _name.text;
			string email = _email.text;
			WriteNewUser(userId, name, email);
		} else {
			return;
		}
		
	}

	public void RetrieveData() {
		if (ConnectionController.Instance().isOnline == true) {

			FirebaseDatabase reference = FirebaseDatabase.DefaultInstance;
			reference.GetReference("Users").GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					// Handle the error...
				} else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					// Do something with snapshot...

					foreach (DataSnapshot ele in snapshot.Children) {
						//Debug.Log();
						userList.Add(JsonUtility.FromJson<User>(ele.GetRawJsonValue()));
					}
					string username = userList[0].username;
					string email = userList[0].email;
					Debug.Log(userList[0].email);


					//string json = snapshot.GetRawJsonValue();
					//Dictionary<string, Dictionary<string, string>> UserDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

					//foreach (var ele in UserDict.Keys) {
					//	Debug.Log("KEY1: " + ele);
					//}

					//foreach (var ele in UserDict.Values) {

					//	Debug.Log(ele["username"]);
					//	Debug.Log(ele["email"]);
					//}
				}
			});
		} else {
			return;
		}
	}

	private void UpdateDB(string userId, string name, string email) {
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		string key = reference.Child("Users").Push().Key;

		User user = new User(UID, name);
		Dictionary<string, Object> userDatas = user.ToDictionary();

		Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
		//childUpdates["/scores/" + key] = childUpdates;
		//childUpdates["/user-scores/" + userId + "/" + key] = childUpdates;

		//mDatabase.UpdateChildrenAsync(childUpdates);
	}
	public void Show() {
		txtData.text = "Username : " + userList[0].username + "\n" + "Email : " + userList[0].email;
	}

	public void UserDataUpdate() {
		string name = _name.text;
		string email = _email.text;
		UpdateDB(userId, name, email);
	}
}
