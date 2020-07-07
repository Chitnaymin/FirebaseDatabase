using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class DBScript : MonoBehaviour
{
	public string userId;
	public InputField _name;
	public InputField _email;
	public Text txtLoadData;

    // Start is called before the first frame update
    void Start()
    {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dbtest-7dd25.firebaseio.com/");
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void writeNewUser(string userId, string name, string email) {
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		User user = new User(name, email);
		string json = JsonUtility.ToJson(user);

		reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
	}

	public void SaveDB() {
		string name = _name.text;
		string email = _email.text;
		writeNewUser(userId,name,email);
	}

	public void RetrieveData() {
	FirebaseDatabase reference=FirebaseDatabase.DefaultInstance;
	 reference.GetReference("users").GetValueAsync().ContinueWith(task => {
		  if (task.IsFaulted) {
			  // Handle the error...
		  } else if (task.IsCompleted) {
			  DataSnapshot snapshot = task.Result;
			 // Do something with snapshot...
			 Debug.Log("Here");	

		 }
	  });
	}
}
