using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatApp
{
    public List<Message> MessageList = new List<Message>();

    /// <summary>
    /// Allows you to send messages to the chatapp from anywhere
    /// </summary>
    /// <param name="message">Message is a string</param>
    /// <param name="sender">Sender is a Message.Sender object</param>
    /// <param name="type">Type is a Message.Type object</param>
    /// <param name="possibleAnswers">List<string> of possible answers</param>
    public void Send(string message, Sender sender, Message.Type type, List<string> possibleAnswers = null)
    {
        MessageList.Add(new Message(message, sender, type, possibleAnswers));
        Debug.Log(sender.name + ": " + message);
    }

    /// <summary>
    /// Get answers of latest message with type == Type.Question
    /// </summary>
    /// <returns>Returns possible answers</returns>
    public List<string> GetAnswers()
    {
        List<string> answers = MessageList.FindLast(M => M.type == Message.Type.Question).possibleAnswers;
        return answers;
    }

    public Message GetLastMessage()
    {
        Message LastMessage = MessageList[MessageList.Count-1];
        return LastMessage;
    }
}

public class Message
{
    public string message;
    public Sender sender;
    public Type type;
    public List<string> possibleAnswers = new List<string>();

    public Message(string message, Sender sender, Type type, List<string> possibleAnswers = null)
    {
        this.message = message;
        this.sender = sender;
        this.type = type;
        this.possibleAnswers = possibleAnswers;
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
