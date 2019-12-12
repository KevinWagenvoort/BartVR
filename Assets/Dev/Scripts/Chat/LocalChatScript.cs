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
    public GameObject NeighbourhoodApp;

    private Sender Jongeren, Jij;
    private List<string> possibleAnswers = new List<string>();
    private int chosenAnswer = 0;
    private ConversationNavigation ConversationNavigationScript;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
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
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();

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
                ConversationNavigationScript.SetChoices(new List<string>() { "Zet die $*#@*&^#@* muziek uit!", "Zouden jullie alsjeblieft de muziek zachter kunnen zetten?", "Muziek uit of ik de bel de politie" });
                break;
            case 1:
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
        switch(scenarioCount)
        {
            case 0:
                NPCTalk("Ah joh, rot op", 3);
                break;
            case 1:
                PlayerTalk("Rot zelf op. Iedereen stoort zich aan jullie");
                break;
            case 2:
                NPCTalk("Bemoei je er niet mee, klootzak", 3);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 3:
                TextBalloon.SetActive(false);
                break;
        }
        scenarioCount++;
    }

    private void AloneFriendly()
    {
        switch (scenarioCount)
        {
            case 0:
                NPCTalk("Ah joh, rot op", 3);
                break;
            case 1:
                PlayerTalk("Doe 's rustig");
                break;
            case 2:
                NPCTalk("Bemoei je er niet mee", 3);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 3:
                TextBalloon.SetActive(false);
                break;
        }
        scenarioCount++;
    }

    private void AloneThreatening()
    {
        switch(scenarioCount)
        {
            case 0:
                NPCTalk("Ah joh, rot op", 3);
                break;
            case 1:
                PlayerTalk("Doe 's rustig");
                break;
            case 2:
                NPCTalk("Jij dreigt. Bemoei je met je eigen zaken", 3);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 3:
                TextBalloon.SetActive(false);
                break;
        }
        scenarioCount++;
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
