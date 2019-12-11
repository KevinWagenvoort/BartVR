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
        Robin = new Sender("Robin", Sender.Role.Npc);
        Jij = new Sender("Jij", Sender.Role.Burger);
        possibleAnswers = new List<string>();
        possibleAnswers.Add("Patatje met doen?");
        possibleAnswers.Add("Pizza time!");
        SendMessageButtonScript = SendMessageButton.GetComponent<SendMessage>();
        Invoke("Tutorial", 2);
    }

    private int passCount = 0;
    public void Tutorial()
    {
        switch (passCount)
        {
            case 0:
                ChatApp.Send("Zullen we vanavond eten halen?", Robin, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 1:
                SendMessageButtonScript.SetMessage("Is goed", Jij, Message.Type.Other);
                break;
            case 2:
                Invoke("Tutorial", 2);
                break;
            case 3:
                ChatApp.Send("Ga jij halen?", Robin, Message.Type.Question, possibleAnswers);
                break;
            case 4:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Snackbar is dicht. Doe maar pizza.", Robin, Message.Type.Other);
                    SendMessageButtonScript.SetMessage("Welke pizzeria?", Jij, Message.Type.Other);
                }
                else
                {
                    ChatApp.Send("Lekker", Robin, Message.Type.Other);
                    SendMessageButtonScript.SetMessage("Waar is die pizzeria?", Jij, Message.Type.Other);
                }
                break;
            case 5:
                Invoke("Tutorial", 2);
                break;
            case 6:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Bartonio's", Robin, Message.Type.Location);
                } else
                {
                    ChatApp.Send("", Robin, Message.Type.Location);
                }
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
