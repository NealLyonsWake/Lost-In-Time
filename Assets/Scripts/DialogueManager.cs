using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


    /// <summary>
    /// Mangages the story segments and diaglogue of the player and NPCs.
    /// Determines if the game is compeleted and ends the application, just for fast demo purposes.
    /// </summary>
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public PlayerMovement _playerMovementScript;
    public GameObject timeController;
    public GameObject villager;
    public GameObject vendButton;
    public AudioSource vendAudio;

    private int currentStorySeg = 0;  
    private string[] dialogueLines;
    private int currentLine = 0;

    void Start()
    {
        dialogueLines = new string[]
        {
            "Where am I?",
            "I'm supposed to meet my dear wife, Aiyana on the bridge in an hour!",
            "We're meeting to celebrate the turn of the year 3000, but this place looks prehistoric!",
            "My Time Gadget must have malfunctioned!",
            "Two Time Components are missing, I need to find them.",
            "[ use A & D to move left and right ]",
            " ",
            "Got them, now, let's jump forward to my time and find Aiyana.",
            " ",
            "Hey, sorry I'm late, you would not believe where I have been!",
            "Aiyana:\nHuh? I think you've got the wrong person, I don't know who you are?",
            "It's me, Arios, your husband!",
            "Aiyana:\nI've never had a husband, sorry but I don't understand who you are.",
            "Oh no, my time gadget must have erased my timeline from history!",
            "I need to find my ancestors and restore myself.",
            "I must have landed in the prehistoric era for a reason.",
            "Let's go back there and see if I can talk with any villagers for clues.",
            " ",
            "Villager:\nPlease help, our elder is sick!",
            "What do you need?",
            "Villager:\nOur elder went hunting for wild boar and was wounded in the hunt.",
            "Villager:\nOur medicine is not strong enough to cure the infection.",
            "Villager:\nI fear there is not much time left!",
            "Okay, I can bring back some medicine from where I am from.",
            "Villager:\nPlease! You have kind eyes like my family and I know you can help.",
            " ",
            "Medi-Vend:\nHello citizen, dispensing Medi-Kit for you now.",
            "Medi-Vend:\nThank you for your custom.",
            "Okay, I now have the medicine.",
            " ",
            "Here, take this, it will cure the infection.",
            "Villager:\nThank you so much, this will save our elder!",
            "Villager:\nI'm sorry, I didn't have time to ask your name?",
            "Sure, my name is Arios Horatius",
            "Villager:\nOh, how strange, we share the same family name, Horatius.",
            "Perhaps I have saved my ancestor I was looking for and restored my timeline!?",
            "Villager:\nWhat?",
            "Oh, sorry, I'm just thinking out loud!",
            "I need to go back to where I belong, take care.",
            "Villager:\nSafe travels on your journey, and thank you.",
            " ",
            "Aiyana:\nWhat took you so long, I missed you!",
            "....",
            "[ Thank you for playing! ]"
        };
        dialogue.text = dialogueLines[currentLine];
    }
    
    public void SetDialogueLine(string npcName)
    {
        _playerMovementScript.SetMove(false, false);
        timeController.SetActive(false);
        if(currentStorySeg == 0)
        {
            currentStorySeg++;
            villager.SetActive(true);
            return;
        }
        if(currentStorySeg == 1 || currentStorySeg == 2 || currentStorySeg == 3 && npcName == "Aiyana_Speech_Button")
        {
            currentLine = 12;
            dialogue.text = dialogueLines[currentLine];
        }
        if(currentStorySeg == 1 && npcName == "Villager_Speech_Button")
        {
            currentStorySeg++;
            currentLine = 18;
            dialogue.text = dialogueLines[currentLine];
            vendButton.SetActive(true);
        }
        else if(currentStorySeg == 2 && npcName == "Villager_Speech_Button")
        {
            currentLine = 23;
            dialogue.text = dialogueLines[currentLine];
        }
        if(currentStorySeg == 2 && npcName == "Vend_Speech_Button")
        {
            vendAudio.Play();
            currentStorySeg++;
            currentLine = 26;
            dialogue.text = dialogueLines[currentLine];
        }
        else if(currentStorySeg > 2 && npcName == "Vend_Speech_Button")
        {
            currentLine = 27;
            dialogue.text = dialogueLines[currentLine];
        }
        if (currentStorySeg == 3 && npcName == "Villager_Speech_Button")
        {
            currentStorySeg++;
            currentLine = 30;
            dialogue.text = dialogueLines[currentLine];
        }
        else if (currentStorySeg > 3 && npcName == "Villager_Speech_Button")
        {
            currentLine = 39;
            dialogue.text = dialogueLines[currentLine];
        }
        if (currentStorySeg > 3 && npcName == "Aiyana_Speech_Button")
        {
            currentLine = 41;
            dialogue.text = dialogueLines[currentLine];
        }
    }

    public void NextLine()
    {
        if (dialogue.text == "[ Thank you for playing! ]") Application.Quit();

        if(currentLine < dialogueLines.Length-1)
        {
            currentLine++;
            dialogue.text = dialogueLines[currentLine];
        }

        if (dialogue.text == " " && currentLine < 43)
        {
            gameObject.SetActive(false);
            currentLine++;
            dialogue.text = dialogueLines[currentLine];
            _playerMovementScript.SetMove(true, false);
            if(currentLine >= 9) timeController.SetActive(true);
        } 
    }
}