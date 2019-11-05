using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKChatAppScript : MonoBehaviour
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
        Sender NPC = new Sender("Vriend", Sender.Role.Npc);
        Sender Jij = new Sender("Jij", Sender.Role.Meldkamer);

        switch (passCount)
        {
            case 0:
                List<string> possibleAnswers = new List<string>();
                possibleAnswers.Add("Test1");
                possibleAnswers.Add("Test2");
                possibleAnswers.Add("Test3");
                ChatApp.Send("Test", NPC, Message.Type.Question, possibleAnswers);
                break;
            case 1:
                ChatApp.Send("Ik kom eraan", NPC, Message.Type.Other);
                break;
            case 2:
                ChatApp.Send(ChatApp.GetAnswers()[0], Jij, Message.Type.Answer);
                break;
            case 3:
                ChatApp.Send("Mooizo", Jij, Message.Type.Other);
                break;
        }
        passCount++;
    }
}
