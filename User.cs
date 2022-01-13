public class User{

    public delegate void Notify(string message);

    public event Notify MessageArrived;

    public string UserName;

    public void SendMessage(string message)
    {
        MessageArrived(message);
    }

}