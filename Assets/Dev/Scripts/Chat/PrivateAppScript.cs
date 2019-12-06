using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateAppScript : MonoBehaviour
{
    public ChatApp ChatApp;
    public GameObject SendMessageButton;

    private Sender Robin;
    private Sender Jij;
    private List<string> possibleAnswers;
    private int chosenAnswer;
    private SendMessage SendMessageButtonScript;

    // Start is called before the first frame update
    void Awake()
    {
        ChatApp = new ChatApp();
        Robin = new Sender("Vriend", Sender.Role.Npc);
        Jij = new Sender("Jij", Sender.Role.Burger);
        possibleAnswers = new List<string>();
        possibleAnswers.Add("Is goed. Ik haal wel pizza. Zie je dan.");
        possibleAnswers.Add("Is goed. Ik haal wel patat. Zie je dan.");
        SendMessageButtonScript = SendMessageButton.GetComponent<SendMessage>();
        Invoke("Tutorial", 2);
    }

    private int passCount = 0;
    public void Tutorial()
    {
        switch (passCount)
        {
            case 0:
                ChatApp.Send("Yo zin om vanavond samen te eten?", Robin, Message.Type.Question, possibleAnswers);
                Invoke("Tutorial", 2);
                break;
            case 1:
                ChatApp.Send("Vanaf 7 uur ben ik thuis. Dan kan je wel komen.", Robin, Message.Type.QuestionFollowup);
                break;
            case 2:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Top, zin in.", Robin, Message.Type.Other);
                }
                else
                {
                    ChatApp.Send("Ik heb liever pizza.", Robin, Message.Type.Other);
                }
                SendMessageButtonScript.SetMessage("Ik ga het nu halen maar ik weet niet waar het is.", Jij, Message.Type.Other);
                break;
            case 3:
                Invoke("Tutorial", 2);
                break;
            case 4:
                ChatApp.Send("Hier is het.", Robin, Message.Type.Location);
                DistanceTrigger.TutorialBurgerIsDone = true;
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
