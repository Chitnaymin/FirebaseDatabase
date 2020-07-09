[System.Serializable]
public class User
{
	public int UID;
	public string username;
	public string email;

	public User(int id,string username, string email) {
		this.UID = id;
		this.username = username;
		this.email = email;
	}
}
