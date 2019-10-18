using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Message;

public static class ChatApp
{
    public static List<Message> MessageList = new List<Message>();

    /// <summary>
    /// Allows you to send messages to the chatapp from anywhere
    /// </summary>
    /// <param name="message">Message is a string</param>
    /// <param name="sender">Sender is a Message.Sender object</param>
    /// <param name="type">Type is a Message.Type object</param>
    /// <param name="possibleAnswers">List<string> of possible answers</param>
    public static void Send(string message, Sender sender, Message.Type type, List<string> possibleAnswers = null)
    {
        MessageList.Add(new Message(message, sender, type, possibleAnswers));
        Debug.Log(sender.name + ": " + message);
    }

    /// <summary>
    /// Get answers of latest message with type == Type.Question
    /// </summary>
    /// <returns>Returns possible answers</returns>
    public static List<string> GetAnswers()
    {
        List<string> answers = MessageList.FindLast(M => M.type == Type.Question).possibleAnswers;
        return answers;
    }

    private static int passCount = 0;
    public static void StartingConversation()
    {
        Sender Vriend = new Sender("Vriend", Sender.Role.Npc);
        Sender Jij = new Sender("Jij", Sender.Role.Burger);

        switch (passCount)
        {
            case 0:
                List<string> possibleAnswers = new List<string>();
                possibleAnswers.Add("Is goed. Ik haal wel pizza. Zie je dan.");
                possibleAnswers.Add("Is goed. Ik haal wel patat. Zie je dan.");
                possibleAnswers.Add("Is goed. Ik haal wel chinees. Zie je dan.");
                Send("Yo zin om vanavond samen te eten? Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Vriend, Type.Question, possibleAnswers);
                break;
            case 1:
                Send("Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Vriend, Type.Other);
                break;
            case 2:
                Send(GetAnswers()[0], Jij, Type.Answer);
                break;
            case 3:
                Send("Top, zin in.", Jij, Type.Other);
                break;
        }
        passCount++;
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
