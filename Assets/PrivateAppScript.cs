using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateAppScript : MonoBehaviour
{
    public ChatApp ChatApp;

    private Sender Vriend;
    private Sender Jij;
    private List<string> possibleAnswers;
    private int chosenAnswer;

    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();
        Vriend = new Sender("Vriend", Sender.Role.Npc);
        Jij = new Sender("Jij", Sender.Role.Burger);
        possibleAnswers = new List<string>();
        possibleAnswers.Add("Is goed. Ik haal wel pizza. Zie je dan.");
        possibleAnswers.Add("Is goed. Ik haal wel patat. Zie je dan.");
        possibleAnswers.Add("Is goed. Ik haal wel chinees. Zie je dan.");
    }

    private int passCount = 0;
    public void Tutorial()
    {
        switch (passCount)
        {
            case 0:
                ChatApp.Send("Yo zin om vanavond samen te eten?", Vriend, Message.Type.Question, possibleAnswers);
                Invoke("Tutorial", 2);
                break;
            case 1:
                ChatApp.Send("Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Vriend, Message.Type.QuestionFollowup);
                break;
            case 2:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Top, zin in.", Vriend, Message.Type.Other);
                }
                else
                {
                    ChatApp.Send("Ik heb liever pizza.", Vriend, Message.Type.Other);
                }
                Invoke("Tutorial", 2);
                break;
            case 3:
                ChatApp.Send("Ik ga het nu halen.", Jij, Message.Type.Other);
                break;
        }
        passCount++;
    }

    public void SendChoice(int messageNumber)
    {
        ChatApp.Send(possibleAnswers[messageNumber], Jij, Message.Type.Answer);
        chosenAnswer = messageNumber;
        Invoke("Tutorial", 2);
    }
}
