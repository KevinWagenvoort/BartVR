using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourhoodAppScript : MonoBehaviour
{
    public ChatApp ChatApp;
    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();
    }

    private int passCount = 0;
    public void TemplateConversation()
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
                ChatApp.Send("Yo zin om vanavond samen te eten?", Vriend, Message.Type.Question, possibleAnswers);
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
}
