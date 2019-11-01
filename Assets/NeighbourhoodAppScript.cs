using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourhoodAppScript : MonoBehaviour
{
    public ChatApp ChatApp;

    private Sender Vriend;
    private Sender Jij;
    private List<string> possibleAnswers = new List<string>();
    private int chosenAnswer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();
        possibleAnswers.Add("Is goedburger. Ik haal wel pizza. Zie je dan.");
        possibleAnswers.Add("Is goed. Ik haal wel patat. Zie je dan.");
        possibleAnswers.Add("Is goed. Ik haal wel chinees. Zie je dan.");
        Vriend = new Sender("Vriend", Sender.Role.Npc);
        Jij = new Sender("Jij", Sender.Role.Burger);
    }

    private int passCount = 0;
    public void TemplateConversation()
    {
        switch (passCount)
        {
            case 0:
                ChatApp.Send("Yo burger om vanavond samen te eten?", Vriend, Message.Type.Question, possibleAnswers);
                break;
            case 1:
                ChatApp.Send("Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Vriend, Message.Type.Other);
                break;
            case 2:
                ChatApp.Send(ChatApp.GetAnswers()[0], Jij, Message.Type.Answer);
                break;
            case 3:
                ChatApp.Send("Top, zin in.", Jij, Message.Type.Other);
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
