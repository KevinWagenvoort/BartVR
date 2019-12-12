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
        possibleAnswers.Add("Wat is het adres van de familie Benjamins?");
        possibleAnswers.Add("Kunt u omschrijven wat u precies heeft gezien?");
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
                ChatApp.Send("Ja, ze zijn eergister weg gegaan", Jong, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 4:
                ChatApp.Send("Zou het een inbreker zijn?", Appel, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 5:
                ChatApp.Send("Moeten we de politie bellen?", Jong, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 6:
                MKChatScript.SetMessage("De meldkamer is op de hoogte van het incident.", Meldkamer, Message.Type.Other);
                break;
            case 7:
                MKChatScript.SetMessage("We zouden u graag een aantal vragen willen stellen.", Meldkamer, Message.Type.Other);
                break;
            case 8:
                ChatApp.Send("Is goed", Appel, Message.Type.QuestionTrigger, possibleAnswers);
                break;
            case 9:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.QuestionTriggerAnswer, possibleAnswers, null, "huissteeg 1");
                } else
                {
                    ChatApp.Send("Een schaduw van een persoon in de woonkamer", Appel, Message.Type.QuestionTriggerAnswer, possibleAnswers, null, "schaduw van persoon in woonkamer");
                }
                break;
            case 10:
                if (chosenAnswer == 0)
                {
                    ChatApp.Send("Huissteeg 1", Appel, Message.Type.Other, null, null, "huissteeg 1");
                }
                else
                {
                    ChatApp.Send("Een schaduw van een persoon in de woonkamer", Appel, Message.Type.Other, null, null, "schaduw van persoon in woonkamer");
                }
                Invoke("Tutorial", 2);
                break;
            case 11:
                MKChatScript.SetMessage("Bedankt voor de informatie. Dit geven we door aan de dichtsbijzijnde eenheid.", Meldkamer, Message.Type.Other);
                break;
            case 12:
                MKChatScript.SetMessage("De politie is nu onderweg.", Meldkamer, Message.Type.Other);
                break;
            case 13:
                MKChatScript.SetMessage("Er was inderdaad sprake van een inbraak. Er is een verdachte aangehouden.", Meldkamer, Message.Type.Other);
                break;
            case 14:
                ChatApp.Send("Gelukkig waren jullie er op tijd bij", Appel, Message.Type.Other);
                Invoke("Tutorial", 2);
                break;
            case 15:
                MKChatScript.SetMessage("We bedanken iedereen voor hun medewerking.", Meldkamer, Message.Type.Other);
                break;
            case 16:
                ChatApp.ClearMessages();
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
        if (!conversationIsDone && scenarioPassCount > 20)
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
        if (!conversationIsDone && scenarioPassCount > 20)
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
                ChatApp.Send("Horen jullie dat ook?", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 1:
                ChatApp.Send("Ja man, wat een lawaai!", Jong, Message.Type.Other, null, null, "lawaai");
                Invoke("Scenario", 2);
                break;
            case 2:
                ChatApp.Send("Echt he, dit slaat echt nergens op", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 3:
                SendMessageButtonScript.SetMessage("Ben onderweg naar Bartonio's. Hoor het ook", Jij, Message.Type.Other, null, null, "bartonio's");
                break;
            case 4:
                SendMessageButtonScript.SetMessage("Staat een groep jongeren met harde muziek", Jij, Message.Type.Other, null, null, "jongeren harde muziek");
                break;
            case 5:
                Invoke("Scenario", 2);
                break;
            case 6:
                ChatApp.Send("Zal ik de wijkagent bellen?", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 7:
                ChatApp.Send("Doe maar", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 8:
                MKChatScript.SetMessage("De meldkamer leest mee.", Meldkamer, Message.Type.Other);
                break;
            case 9:
                MKChatScript.SetMessage("Zou iemand de jongeren aan kunnen spreken?", Meldkamer, Message.Type.Other);
                break;
            case 10:
                SendMessageButtonScript.SetMessage("Ik kan wel helpen", Jij, Message.Type.Other);
                break;
            case 11:
                Invoke("Scenario", 2);
                break;
            case 12:
                MKChatScript.SetMessage("Bedankt voor de medewerking.", Meldkamer, Message.Type.Other);
                break;
            case 13:
                MKChatScript.SetMessage("Zou u een foto van de situatie kunnen maken?", Meldkamer, Message.Type.PhotoRequest);
                break;
            case 14:
                //Keep empty
                break;
            case 15:
                MKChatScript.SetMessage("Bedankt, kunt u met de jongeren in gesprek gaan?", Meldkamer, Message.Type.Other);
                break;
            case 16:
                SendMessageButtonScript.SetMessage("Ja hoor, ik loop nu op ze af", Jij, Message.Type.Other);
                break;
            case 17:
                DistanceTrigger.StartScenarioDone = true;
                Debug.Log("End intro");
                break;
            case 18:
                Debug.Log("Start 2nd part scenario");
                ChatApp.Send("Gaat alles goed daar?", Jong, Message.Type.Other);
                break;
            case 19:
                SendMessageButtonScript.SetMessage("Ze hebben een raam ingegooid!", Jij, Message.Type.Other, null, null, "raam ingegooid");
                break;
            case 20:
                Invoke("Scenario", 2);
                break;
            case 21:
                ChatApp.Send("Wat een klootzakken!", Beer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 22:
                ChatApp.Send("Er moet ingegrepen worden", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 23:
                MKChatScript.SetMessage("We lezen nog steeds mee.", Meldkamer, Message.Type.Other);
                break;
            case 24:
                MKChatScript.SetMessage("Houd alstublieft afstand.", Meldkamer, Message.Type.Other);
                break;
            case 25:
                SendMessageButtonScript.SetMessage("Ik ga er weg", Jij, Message.Type.Other);
                break;
            case 26:
                MKChatScript.SetMessage("We zullen u een aantal vragen stellen.", Meldkamer, Message.Type.Other);
                break;
            case 27:
                possibleAnswers = new List<string>();
                possibleAnswers.Add("Hebben de jongeren nog meer vernield?");
                possibleAnswers.Add("Om hoeveel jongeren gaat het?");
                possibleAnswers.Add("Zou u een foto kunnen maken?");
                possibleAnswers.Add("Wie heeft de steen gegooid?");
                possibleAnswers.Add("Waar bevindt de kapotte ruit zich?");
                SendMessageButtonScript.SetMessage("Is goed", Jij, Message.Type.QuestionTrigger, possibleAnswers);
                break;
            case 28:
                //Keep empty
                break;
            case 29:
                AskQuestionToCitizen();
                break;
            case 30:
                AskQuestionToCitizen();
                break;
            case 31:
                AskQuestionToCitizen();
                break;
            case 32:
                AskQuestionToCitizen();
                break;
            case 33:
                AskQuestionToCitizen();
                conversationIsDone = true;
                break;
            case 34:
                MKChatScript.SetMessage("Bedankt voor de informatie.", Meldkamer, Message.Type.Other);
                break;
            case 35:
                MKChatScript.SetMessage("De politie is onderweg naar uw locatie.", Meldkamer, Message.Type.Other);
                break;
            case 36:
                SendMessageButtonScript.SetMessage("Moet ik verder nog wat doen?", Jij, Message.Type.Other);
                break;
            case 37:
                MKChatScript.SetMessage("Blijf op een veilige afstand staan.", Meldkamer, Message.Type.Other);
                break;
            case 38:
                MKChatScript.SetMessage("De politie zal ter plaatse onderzoek verrichten.", Meldkamer, Message.Type.Other);
                break;
            case 39:
                DistanceTrigger.ScenarioIsDone = true;
                break;
            case 40:
                ChatApp.Send("Bedankt voor uw medewerking.", Meldkamer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 41:
                ChatApp.Send("De politie heeft het incident afgehandeld.", Meldkamer, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 42:
                ChatApp.Send("Bedankt voor het snel oplossen", Appel, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 43:
                ChatApp.Send("BART! werkt echt goed", Jong, Message.Type.Other);
                Invoke("Scenario", 2);
                break;
            case 44:
                ChatApp.Send("Bedankt Bart", Beer, Message.Type.Other);
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
                citizenAnswers.Add("3");
                citizenAnswers.Add("5");
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.Question, citizenAnswers);
                break;
            case 2:
                // Open camera button
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.PhotoRequest);
                break;
            case 3:
                citizenAnswers = new List<string>();
                citizenAnswers.Add("Man met blond haar en blauwe blouse");
                citizenAnswers.Add("Vrouw, zwart haar en sport shirt");
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.Question, citizenAnswers);
                break;
            case 4:
                citizenAnswers = new List<string>();
                citizenAnswers.Add("Voorkant van het gebouw");
                citizenAnswers.Add("Achterkant van het gebouw");
                ChatApp.Send(possibleAnswers[chosenAnswer], Meldkamer, Message.Type.Question, citizenAnswers);
                break;
        }
    }
}
