using System.Collections.Generic;
using System;

public class UserManager
{
    public Dictionary<string, User> Users= new Dictionary<string, User>();

    public void AddUsers(string username){

        if(Users.ContainsKey(username))
        {
            return;
        }

        Users.Add(username, new User{UserName=username});
        Console.WriteLine("Users--------");
        foreach(var user in Users){
            Console.WriteLine(user.Value.UserName);
        }
        Console.WriteLine("--------");
    }

    public void SendMessage(string message)
    {
        foreach(var user in Users)
        {
            Console.WriteLine(user.Value.UserName);
            user.Value.SendMessage(message);
        }
    }

}