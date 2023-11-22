[System.Serializable]
public class UserData
{
    public int user_id;
    public string name;
    public int[] inventory;

    public UserData(int user_id, string name, int[] inventory)
    {
        this.user_id = user_id;
        this.name = name;
        this.inventory = inventory;
    }
}