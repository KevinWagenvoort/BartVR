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

    [Header("Audio objects")]
    public AudioSource PlayerSpeaker;
    public AudioSource SuspectSpeaker;
    public AudioSource Music;

    public List<AudioClip> FriendlyAudioClips;
    public List<AudioClip> ThreateningAudioClips;
    public List<AudioClip> AgressiveAudioClips;

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
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
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
                Music.volume = 0.1f;
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
                AudioClip audioClip = AgressiveAudioClips[0];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 1:
                NPCTalk("Ah joh, rot op", AgressiveAudioClips[1]);
                break;
            case 2:
                PlayerTalk("Rot zelf op. Iedereen stoort zich aan jullie");
                break;
            case 3:
                audioClip = AgressiveAudioClips[2];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 4:
                NPCTalk("Bemoei je er niet mee, klootzak", AgressiveAudioClips[3]);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 5:
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
                AudioClip audioClip = FriendlyAudioClips[0];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 1:
                NPCTalk("Ah joh, rot op", FriendlyAudioClips[1]);
                break;
            case 2:
                PlayerTalk("Doe 's rustig");
                break;
            case 3:
                audioClip = FriendlyAudioClips[2];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 4:
                NPCTalk("Bemoei je er niet mee", FriendlyAudioClips[3]);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 5:
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
                AudioClip audioClip = ThreateningAudioClips[0];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 1:
                NPCTalk("Ah joh, rot op", ThreateningAudioClips[1]);
                break;
            case 2:
                PlayerTalk("Doe 's rustig");
                break;
            case 3:
                audioClip = ThreateningAudioClips[2];
                PlayerSpeaker.clip = audioClip;
                PlayerSpeaker.Play();
                Invoke("DefaultPath", audioClip.length);
                break;
            case 4:
                NPCTalk("Jij dreigt. Bemoei je met je eigen zaken", ThreateningAudioClips[3]);
                NeighbourhoodAppScript.Scenario((int)ToneType);
                Phone.SetActive(true);
                LeftHand.SetActive(false);
                DistanceTrigger.ConversationIsDone = true;
                break;
            case 5:
                TextBalloon.SetActive(false);
                break;
        }
        scenarioCount++;
    }

    private void DefaultPath ()
    {
        path();
    }

    private void NPCTalk(string message, AudioClip audioClip)
    {
        TextBalloon.SetActive(true);
        BalloonText.text = message;
        SuspectSpeaker.clip = audioClip;
        SuspectSpeaker.Play();
        PlayerSpeaker.Stop();
        Invoke("DefaultPath", audioClip.length);
    }

    private void PlayerTalk(string message)
    {
        TextBalloon.SetActive(false);
        ConversationNavigationScript.SetChoice(message);
    }
}
