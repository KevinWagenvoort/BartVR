using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

delegate void ScenarioPath();

public class LocalChatScript : MonoBehaviour
{
    public ChatApp ChatApp;
    public GameObject ChoiceBubbles;
    public GameObject TextBalloon, LeftHand, Phone;
    public TMP_Text BalloonText;

    private Sender Jongeren, Jij;
    private List<string> possibleAnswers = new List<string>();
    private int chosenAnswer = 0;
    private ConversationNavigation ConversationNavigationScript;
    private enum Tone {
        Aggressive,
        Friendly,
        Threatening
    }
    private Tone ToneType;
    private bool isAlone = true;
    private int scenarioCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChatApp = new ChatApp();

        //Senders
        Jongeren = new Sender("Jongeren", Sender.Role.Npc);
        Jij = new Sender("Bart van Rijn", Sender.Role.Burger);

        possibleAnswers = new List<string>();
        possibleAnswers.Add("Wat is het adres?");
        possibleAnswers.Add("Kunt u omschrijven wat u precies ziet?");
    }

    private void Awake()
    {
        ConversationNavigationScript = ChoiceBubbles.GetComponent<ConversationNavigation>();
    }

    private int passCount = 0;
    ScenarioPath path;
    public void PizzaSuspects()
    {
        path = PizzaSuspects;
        switch (passCount)
        {
            case 0:
                ConversationNavigationScript.SetChoices(new List<string>() { "Zet die pokke herrie uit anders haal ik de politie!", "Zouden jullie de muziek zachter willen zetten?", "Ik heb graag dat jullie weggaan anders bel ik de politie!" });
                break;
            case 1:
                Debug.Log("Case 1");
                ToneType = (Tone)chosenAnswer;
                if (isAlone)
                {
                    switch(ToneType)
                    {
                        case Tone.Friendly:
                            path = AloneFriendly;
                            break;
                        case Tone.Aggressive:
                            path = AloneAgressive;
                            break;
                        case Tone.Threatening:
                            path = AloneThreatening;
                            break;
                    }
                } else
                {
                    switch (ToneType)
                    {
                        case Tone.Friendly:
                            path = TogetherFriendly;
                            break;
                        case Tone.Aggressive:
                            path = TogetherAgressive;
                            break;
                        case Tone.Threatening:
                            path = TogetherThreatening;
                            break;
                    }
                }
                path();
                break;
            
        }
        passCount++;
    }

    public void SendChoice(int messageNumber)
    {
        chosenAnswer = messageNumber;
        Invoke("DefaultPath", 1);
    }

    private void AloneAgressive()
    {
        Debug.Log("AloneAgressive");
        switch(scenarioCount)
        {
            case 0:
                PlayerTalk("Jullie hebben de herrie veel te hard");
                break;
            case 1:
                PlayerTalk("Zet het uit");
                break;
            case 2:
                NPCTalk("Nee nergens voor nodig", 2);
                break;
            case 3:
                NPCTalk("We hebben gewoon plezier", 2);
                break;
            case 4:
                NPCTalk("Dat laten we niet door jou verpesten", 2);
                break;
            case 5:
                PlayerTalk("Het is toch niet zo lastig om dat lawaai te verminderen");
                break;
            case 6:
                PlayerTalk("Iedereen heeft er last van");
                break;
            case 7:
                NPCTalk("Kan ons weinig schelen", 2);
                break;
            case 8:
                NPCTalk("Jullie bekijken het maar", 2);
                break;
            case 9:
                PlayerTalk("Dan stuur ik de politie op jullie af");
                break;
            case 10:
                NPCTalk("Je doet maar", 2);
                break;
            case 11:
                NPCTalk("Boeit ons toch niet", 2);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
        }
        scenarioCount++;
    }

    private void AloneFriendly()
    {
        Debug.Log("AloneFriendly");
        switch (scenarioCount)
        {
            case 0:
                PlayerTalk("Jullie zijn namelijk best wel luid en bewoners hebben er last van");
                break;
            case 1:
                NPCTalk("Waar heb je het over?", 2);
                break;
            case 2:
                PlayerTalk("De muziek staat best hard. Zouden jullie het zachter willen zetten?");
                break;
            case 3:
                NPCTalk("Valt reuze mee", 2);
                break;
            case 4:
                NPCTalk("Zo luid is het helemaal niet", 2);
                break;
            case 5:
                PlayerTalk("Het zorgt in ieder geval wel voor overlast");
                break;
            case 6:
                NPCTalk("We zijn hier gewoon gezellig aan het chillen", 2);
                break;
            case 7:
                NPCTalk("Mag toch ook wel eens?", 2);
                break;
            case 8:
                PlayerTalk("Tuurlijk mag dat maar het moet wel wat zachter");
                break;
            case 9:
                NPCTalk("Wat een gezeur weer", 2);
                break;
            case 10:
                NPCTalk("Vooruit dan maar", 2);
                break;
            case 11:
                NPCTalk("We doen de muziek zachter", 2);
                break;
            case 12:
                PlayerTalk("Bedankt jongens");
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;

        }
        scenarioCount++;
    }

    private void AloneThreatening()
    {
        Debug.Log("AloneThreatening");
        switch(scenarioCount)
        {
            case 0:
                NPCTalk("Wow oke, chill. is het zo luid dan?", 2);
                break;
            case 1:
                PlayerTalk("Ja, het is een paar straten verderop nog te horen");
                break;
            case 2:
                NPCTalk("oh, dat is niet onze bedoeling", 2);
                break;
            case 3:
                NPCTalk("We wouden hier gewoon lekker chillen", 2);
                break;
            case 4:
                PlayerTalk("Zouden jullie het iets zachter willen zetten?");
                break;
            case 5:
                PlayerTalk("Misschien is het handig als jullie in het park gaan zitten.");
                break;
            case 6:
                PlayerTalk("Daar hebben mensen er minder last van");
                break;
            case 7:
                NPCTalk("Dan gaan we wel naar het park toe. Wel zo handig", 2);
                break;
            case 8:
                NPCTalk("Dan hebben jullie ook nergens last van", 2);
                break;
            case 9:
                PlayerTalk("Heel erg bedankt. Leuke avond verder toegewenst");
                break;
            case 10:
                NPCTalk("Geen probleem joh", 2);
                break;
            case 11:
                NPCTalk("Fijn dat jullie zo rustig even met ons zijn komen praten", 2);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;

        }
        scenarioCount++;
    }

    private void TogetherAgressive()
    {
        Debug.Log("TogetherAgressive");
    }

    private void TogetherFriendly()
    {
        Debug.Log("TogetherFriendly");
    }

    private void TogetherThreatening()
    {
        Debug.Log("TogetherThreatening");
    }

    private void DefaultPath ()
    {
        path();
    }

    private void NPCTalk(string message, int seconds = 1)
    {
        TextBalloon.SetActive(true);
        BalloonText.text = message;
        Invoke("DefaultPath", seconds);
    }

    private void PlayerTalk(string message)
    {
        TextBalloon.SetActive(false);
        ConversationNavigationScript.SetChoice(message);
    }
}
