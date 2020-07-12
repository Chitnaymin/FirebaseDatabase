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
	public Slider LoadData;
	private int UID;

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
		string name = _name.text;
		string email = _email.text;
		WriteNewUser(userId, name, email);
	}

	public void RetrieveData() {
		FirebaseDatabase reference = FirebaseDatabase.DefaultInstance;
		reference.GetReference("Users").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				// Handle the error...
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				// Do something with snapshot...
				List<User> userList = new List<User>();
				foreach (DataSnapshot ele in snapshot.Children) {
					Debug.Log(" what ");
					//Debug.Log();
					userList.Add(JsonUtility.FromJson<User>(ele.GetRawJsonValue()));
				}
				foreach(User user in userList) {
					string username= user.username;
				}
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

	public void UserDataUpdate() {
		string name = _name.text;
		string email = _email.text;
		UpdateDB(userId, name, email);
	}
}
