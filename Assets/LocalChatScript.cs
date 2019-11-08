using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

delegate void ScenarioPath();

public class LocalChatScript : MonoBehaviour
{
    public ChatApp ChatApp;
    public GameObject ChoiceBubbles;
    public GameObject ControllerLeft;
    public GameObject TextBalloon;
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
        ControllerLeft.SetActive(true);
        ChatApp = new ChatApp();
        ConversationNavigationScript = ChoiceBubbles.GetComponent<ConversationNavigation>();

        //Senders
        Jongeren = new Sender("Jongeren", Sender.Role.Npc);
        Jij = new Sender("Bart van Rijn", Sender.Role.Burger);

        possibleAnswers = new List<string>();
        possibleAnswers.Add("Wat is het adres?");
        possibleAnswers.Add("Kunt u omschrijven wat u precies ziet?");
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
                ConversationNavigationScript.SetChoice("Jullie hebben de herrie veel te hard");
                break;
            case 1:
                ConversationNavigationScript.SetChoice("Zet het uit");
                break;
            case 2:
                TextBalloon.SetActive(true);
                BalloonText.text = "Nee nergens voor nodig";
                Invoke("DefaultPath", 2);
                break;
            case 3:
                BalloonText.text = "We hebben gewoon plezier";
                Invoke("DefaultPath", 2);
                break;
            case 4:
                BalloonText.text = "Dat laten we niet door jou verpesten";
                Invoke("DefaultPath", 2);
                break;
            case 5:
                TextBalloon.SetActive(false);
                ConversationNavigationScript.SetChoice("Het is toch niet zo lastig om dat lawaai te verminderen");
                break;
            case 6:
                ConversationNavigationScript.SetChoice("Iedereen heeft er last van");
                break;
            case 7:
                TextBalloon.SetActive(true);
                BalloonText.text = "Kan ons weinig schelen";
                Invoke("DefaultPath", 2);
                break;
            case 8:
                BalloonText.text = "Jullie bekijken het maar";
                Invoke("DefaultPath", 2);
                break;
            case 9:
                TextBalloon.SetActive(false);
                ConversationNavigationScript.SetChoice("Dan stuur ik de politie op jullie af");
                break;
            case 10:
                TextBalloon.SetActive(true);
                BalloonText.text = "Je doet maar";
                Invoke("DefaultPath", 2);
                break;
            case 11:
                BalloonText.text = "Boeit ons toch niet";
                Invoke("DefaultPath", 2);
                break;
        }
        scenarioCount++;
    }

    private void AloneFriendly()
    {
        Debug.Log("AloneFriendly");
    }

    private void AloneThreatening()
    {
        Debug.Log("AloneThreatening");
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
}
