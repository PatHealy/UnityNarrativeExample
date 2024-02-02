using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//the ink package
using Ink.Runtime;
using System;

public class NPCDialogue : MonoBehaviour
{
    public Transform cameraPosition, cameraTarget;

    public event Action<Story> OnCreateStory;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    // UI Prefab
    [SerializeField]
    private Button buttonPrefab = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartThisDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndThisDialogue();
        }
    }

    public void StartThisDialogue()
    {
        CameraBehavior.instance.FocusCamera(cameraTarget, cameraPosition.position);
        StartStory();
    }

    public void EndThisDialogue()
    {
        CameraBehavior.instance.RestoreFollowPlayer();
        EndStory();
    }

    /*
     * ================================== 
     * Ink functions
     * ==================================
     */

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    void EndStory()
    {
        story = null;
        RemoveChildren();
    }

    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate {
                StartStory();
            });
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    public void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Pass the text on to the dialogue camera
    void CreateContentView(string text)
    {
        CameraBehavior.instance.SetDialogueText(text);
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        CameraBehavior.instance.AddChoice(choice);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        CameraBehavior.instance.ClearText();
        CameraBehavior.instance.ClearChoices();
    }
}
