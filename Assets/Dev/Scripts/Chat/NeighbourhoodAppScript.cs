using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourhoodAppScript : MonoBehaviour
{
    public ChatApp ChatApp;
    public GameObject MKChat;

    private Sender Appel, Jij, Beer, Jong, Meldkamer;
    private List<string> possibleAnswers = new List<string>();
    private int chosenAnswer = 0;
    private MKChat MKChatScript;

    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();
        MKChatScript = MKChat.GetComponent<MKChat>();

        //Senders
        Appel = new Sender("Daphne Appeltje", Sender.Role.Npc);
        Jij = new Sender("Bart van Rijn", Sender.Role.Burger);
        Beer = new Sender("Bernard Beer", Sender.Role.Npc);
        Jong = new Sender("Marc de Jong", Sender.Role.Npc);
        Meldkamer = new Sender("Meldkamer", Sender.Role.Meldkamer);

        possibleAnswers = new List<string>();
        possibleAnswers.Add("Wat is het adres?");
        possibleAnswers.Add("Kunt u omschrijven wat u precies ziet?");
        Tutorial();
    }

    private int passCount = 0;
    public void Tutorial()
    {
        switch (passCount)
        {
            case 0:
                ChatApp.Send("Ik zie iemand in het huis van familie Benjamins.", Appel, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 1:
                ChatApp.Send("Ze zijn toch op vakantie?", Appel, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 2:
                ChatApp.Send("Dat dacht ik ook.", Beer, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 3:
                ChatApp.Send("Er is iets niet pluis.", Jong, Message.Type.QuestionTrigger, possibleAnswers);
                break;
            case 4:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.QuestionTrigger, possibleAnswers);
                } else
                {
                    ChatApp.Send("Ik denk dat ik een inbreker binnen zie.", Appel, Message.Type.QuestionTrigger, possibleAnswers);
                }
                break;
            case 5:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.Other);
                }
                else
                {
                    ChatApp.Send("Ik denk dat ik een inbreker binnen zie.", Appel, Message.Type.Other);
                }
                Invoke("Tutorial", 2);
                break;
            case 6:
                MKChatScript.SetMessage("We sturen er direct een agent naartoe.", Meldkamer, Message.Type.Other);
                break;
            case 7:
                MKChatScript.SetMessage("Er was inderdaad sprake van een inbraak.", Meldkamer, Message.Type.Other);
                break;
            case 8:
                MKChatScript.SetMessage("De Inbreker is opgepakt.", Meldkamer, Message.Type.Other);
                break;
            case 9:
                MKChatScript.SetMessage("Bedankt voor jullie medewerking.", Meldkamer, Message.Type.Other);
                break;
            case 10:
                ChatApp.Send("Jullie bedankt voor het oppakken van de inbreker.", Appel, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 11:
                ChatApp.Send("Gelukkig waren we er op tijd bij.", Jong, Message.Type.Other);
                break;
        }
        passCount++;
    }

    public void SendChoice(int messageNumber)
    {
        ChatApp.Send(possibleAnswers[messageNumber], Meldkamer, Message.Type.Other);
        chosenAnswer = messageNumber;
        Invoke("Tutorial", 2);
    }

    public void SendPhoto(Sprite photo)
    {
        ChatApp.Send("", Jij, Message.Type.Photo, null, photo);
    }

    public void SendMessage(Message message)
    {
        ChatApp.Send(message);
        Invoke("Tutorial", 2);
    }
}
