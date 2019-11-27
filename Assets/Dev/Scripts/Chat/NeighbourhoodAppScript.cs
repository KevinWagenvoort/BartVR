using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeighbourhoodAppScript : MonoBehaviour
{
    public ChatApp ChatApp;
    public GameObject MKChat, SendMessageButton, IncidentController, PizzaLocation;
    public TMP_Text GroupChatName;

    private Sender Appel, Jij, Beer, Jong, Meldkamer;
    private List<string> possibleAnswers = new List<string>();
    private int chosenAnswer = 0;
    private MKChat MKChatScript;
    private SendMessage SendMessageButtonScript;
    private string currentScenario = "Tutorial";
    private IncidentController IncidentControllerScript;
    private bool conversationIsDone = false;
    private int ToneType = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();
        MKChatScript = MKChat.GetComponent<MKChat>();
        SendMessageButtonScript = SendMessageButton.GetComponent<SendMessage>();
        IncidentControllerScript = IncidentController.GetComponent<IncidentController>();

        //Senders
        Appel = new Sender("Daphne Appeltje", Sender.Role.Npc);
        Jij = new Sender("Bart van Rijn", Sender.Role.Burger);
        Beer = new Sender("Bernard Beer", Sender.Role.Npc);
        Jong = new Sender("Marc de Jong", Sender.Role.Npc);
        Meldkamer = new Sender("Meldkamer", Sender.Role.Meldkamer);

        possibleAnswers = new List<string>();
        possibleAnswers.Add("Wat is het adres?");
        possibleAnswers.Add("Kunt u omschrijven wat u precies ziet?");
    }

    private int passCount = 0;
    public void Tutorial()
    {
        switch (passCount)
        {
            case 0:
                GroupChatName.text = "Buurtapp De huistegers";
                ChatApp.Send("Ik zie iemand in het huis van familie Benjamins.", Appel, Message.Type.Other, null, null, "iemand in huis");
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
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.QuestionTriggerAnswer, possibleAnswers, null, "huissteeg 1");
                } else
                {
                    ChatApp.Send("Ik denk dat ik een inbreker binnen zie.", Appel, Message.Type.QuestionTriggerAnswer, possibleAnswers, null, "inbreker");
                }
                break;
            case 5:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.Other, null, null, "huissteeg 1");
                }
                else
                {
                    ChatApp.Send("Ik denk dat ik een inbreker binnen zie.", Appel, Message.Type.Other, null, null, "inbreker");
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
                DistanceTrigger.TutorialControlRoomIsDone = true;
                IncidentControllerScript.ResetMK();
                MKChatScript.ResetMK();
                currentScenario = "Scenario";
                break;
        }
        passCount++;
    }

    public void SendChoice(int messageNumber)
    {
        chosenAnswer = messageNumber;
        if (currentScenario != "Scenario")
        {
            ChatApp.Send(possibleAnswers[messageNumber], Meldkamer, Message.Type.Other);
            Invoke(currentScenario, 2);
        }
        else
        {
            Scenario();
        }
    }

    public void SendChoiceCitizen(string text)
    {
        if (!conversationIsDone)
        {
            ChatApp.Send(text, Jij, Message.Type.QuestionTriggerAnswer, possibleAnswers);
        } else
        {
            ChatApp.Send(text, Jij, Message.Type.Other);
            Invoke("Scenario", 1);
        }
    }

    public void SendPhoto(Sprite photo)
    {
        if (!conversationIsDone)
        {
            ChatApp.Send("", Jij, Message.Type.PhotoQuestionTrigger, possibleAnswers, photo);
        } else
        {
            ChatApp.Send("", Jij, Message.Type.Photo, null, photo);
            Invoke("Scenario", 1);
        }
    }

    public void SendMessage(Message message)
    {
        ChatApp.Send(message);
        if (message.type != Message.Type.QuestionTrigger)
        {
            Invoke(currentScenario, 2);
        }
    }

    private int scenarioPassCount = 0;
    public void Scenario(int ToneType) {
        this.ToneType = ToneType;
        Scenario();
    }
    public void Scenario()
    {
        switch(scenarioPassCount)
        {
            case 0:
                GroupChatName.text = "Buurtapp Pizzalanden";
                ChatApp.Send("Horen jullie dat lawaai ook?", Appel, Message.Type.Other, null, null, "lawaai");
                Invoke("Scenario", 2);
                break;
            case 1:
                ChatApp.Send("Ja, ik hoor het ook", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 2:
                ChatApp.Send("Ja, het komt van een paar straten verderop", Beer, Message.Type.Other, null, null, "van paar straten verderop");
                Invoke("Scenario", 2);
                break;
            case 3:
                ChatApp.Send("Onze honden worden er helemaal gek van", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 4:
                ChatApp.Send("Moeten we de politie bellen?", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 5:
                ChatApp.Send("Ja, misschien kunnen zij het oplossen", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 6:
                MKChatScript.SetMessage("Wij zullen kijken of we kunnen helpen", Meldkamer, Message.Type.Other);
                break;
            case 7:
                MKChatScript.SetMessage("Is er iemand in de buurt die even kan kijken?", Meldkamer, Message.Type.Other);
                break;
            case 8:
                ChatApp.Send("Nee, ik moet zo weg", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 9:
                SendMessageButtonScript.SetMessage("Ik kan wel even kijken", Jij, Message.Type.Other);
                break;
            case 10:
                SendMessageButtonScript.SetMessage("Ik ben er nu in de buurt en kan ze zien", Jij, Message.Type.Other);
                break;
            case 11:
                Debug.Log("End intro");
                break;
            case 12:
                Debug.Log("Start 2nd part scenario");
                switch (ToneType)
                {
                    case 0://Aggressive
                        SendMessageButtonScript.SetMessage("Ik loop nu weg, ze luisteren voor geen meter!", Jij, Message.Type.Other);
                        break;
                    case 1://Friendly
                        SendMessageButtonScript.SetMessage("Ik loop nu weg, ze gaan de muziek zachter zetten.", Jij, Message.Type.Other);
                        break;
                    case 2://Threatening
                        SendMessageButtonScript.SetMessage("Ik loop nu weg, ze gaan nu naar het park.", Jij, Message.Type.Other);
                        break;
                }
                break;
            case 13:
                Debug.Log("Start walking away");
                break;
            case 14:
                SendMessageButtonScript.SetMessage("Ze hebben een ruit ingegooid", Jij, Message.Type.Other, null, null, "ruit ingegooid");
                break;
            case 15:
                Invoke("Scenario", 2);
                break;
            case 16:
                ChatApp.Send("WAT?!?!", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 17:
                ChatApp.Send("HOE DURVEN ZE", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 18:
                ChatApp.Send("NEE!!!", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 19:
                ChatApp.Send("VAN BARTONIO'S?", Jong, Message.Type.Other, null, null, "bartonio's");
                Invoke("Scenario", 2);
                break;
            case 20:
                SendMessageButtonScript.SetMessage("Ja, met een steen", Jij, Message.Type.Other, null, null, "steen");
                break;
            case 21:
                Invoke("Scenario", 2);
                break;
            case 22:
                ChatApp.Send("NIET NORMAAL DIT", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 23:
                ChatApp.Send("Ze moeten echt 's normaal doen", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 24:
                ChatApp.Send("Eerst al overlast met hun muziek en nu dit!!!", Beer, Message.Type.Other, null, null, "overlast muziek");
                Invoke("Scenario", 2);
                break;
            case 25:
                ChatApp.Send("Dit kan echt niet!!!", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 26:
                SendMessageButtonScript.SetMessage("Ze zijn veel te agressief", Jij, Message.Type.Other, null, null, "agressief");
                break;
            case 27:
                SendMessageButtonScript.SetMessage("De politie moet er nu wel echt bij komen", Jij, Message.Type.Other);
                break;
            case 28:
                Invoke("Scenario", 2);
                break;
            case 29:
                ChatApp.Send("Ze moeten echt aangepakt worden", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 30:
                ChatApp.Send("Arme Bartonio's", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 31:
                possibleAnswers = new List<string>();
                possibleAnswers.Add("Hebben de jongeren naast de ruit nog meer vernield?");
                possibleAnswers.Add("Om hoeveel jongeren gaat het?");
                possibleAnswers.Add("Zou u een foto kunnen maken van de huidige situatie?");
                MKChatScript.SetMessage("We zullen u een aantal vragen stellen zodat de agent genoeg informatie heeft", Meldkamer, Message.Type.QuestionTrigger, possibleAnswers);
                break;
            case 32:
                AskQuestionToCitizen();
                break;
            case 33:
                AskQuestionToCitizen();
                break;
            case 34:
                AskQuestionToCitizen();
                conversationIsDone = true;
                break;
            case 35:
                MKChatScript.SetMessage("Bedankt, de agent is onderweg", Meldkamer, Message.Type.Other);
                DistanceTrigger.ScenarioIsDone = true;
                //GameObject Officer = GameObject.FindGameObjectsWithTag("Officer")[0];
                //Officer.GetComponent<NPCBehaviour>().MoveToTarget(PizzaLocation);
                break;
        }
        scenarioPassCount++;
    }

    void AskQuestionToCitizen()
    {
        List<string> citizenAnswers;
        switch (chosenAnswer)
        {
            case 0:
                citizenAnswers = new List<string>();
                citizenAnswers.Add("Ja, ik zie ook een kapotte prullenbak");
                citizenAnswers.Add("Nee, alleen de ruit");
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.Question, citizenAnswers);
                break;
            case 1:
                citizenAnswers = new List<string>();
                citizenAnswers.Add("Drie");
                citizenAnswers.Add("Vijf");
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.Question, citizenAnswers);
                break;
            case 2:
                // Open camera button
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.PhotoRequest);
                break;
        }
    }
}
