using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatApp : MonoBehaviour
{
    public List<Message> MessageList = new List<Message>();

    public void Send(string message, Sender sender, Message.Type type)
    {
        MessageList.Add(new Message(message, sender, type));
    }

    private void Start()
    {
        TestConversation();

        foreach (Message message in MessageList) {
            Debug.Log(message);
        };
    }

    //Voorbeeld conversatie
    public void TestConversation()
    {
        Sender Vriend = new Sender("Vriend", Sender.Role.Npc);
        Sender Jij = new Sender("Jij", Sender.Role.Burger);

        Send("Yo zin om vanavond samen te eten?", Vriend, Message.Type.Question);
        Send("Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Jij, Message.Type.Other);
        Send("Is goed. Ik haal wel pizza. Zie je dan.", Jij, Message.Type.Answer);
        Send("Top, zin in.", Jij, Message.Type.Other);
    }
}

public class Message
{
    public string message;
    public Sender sender;
    public Type type;

    public Message(string message, Sender sender, Type type)
    {
        this.message = message;
        this.sender = sender;
        this.type = type;
    }

    public override string ToString()
    {
        return "Type: " + type.ToString() + " Verzonden door: " + sender + " Bericht: " + message;
    }

    public enum Type
    {
        Question,
        Answer,
        Other
    }
}

public class Sender
{
    public string name;
    public Role role;

    public Sender(string name, Role role)
    {
        this.name = name;
        this.role = role;
    }

    public enum Role
    {
        Burger,
        Meldkamer,
        Npc
    }
}
