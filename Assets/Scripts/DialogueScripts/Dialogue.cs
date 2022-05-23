using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkFile;
    [SerializeField]
    private TextMeshProUGUI textComponent;
    [SerializeField]
    private GameObject choicesPanel;
    [SerializeField]
    private GameObject choicesPrefab;

    private Story story;
    private Choice choiceSelected;
    private bool isMakingChoice;
    private string currentLine;
    private const float textSpeed = 0.3f;

    /** 
     * OnEnable is called when object becomes enabled and active. 
     * Dialogue restarts if object is interacted again.
     */
    private void OnEnable()
    {
        this.story = new Story(this.inkFile.text);
        this.textComponent.text = string.Empty;
        this.choiceSelected = null;
        this.isMakingChoice = false;
        StartDialogue();
    }

    /** Update is called once per frame. */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Proceed to next dialogue line only if our current
            // line is completed and we are not making a choice.
            if (this.textComponent.text == this.currentLine && !this.isMakingChoice)
            {
                NextLine();
            }
            // Complete the current dialogue line if 'E' is pressed.
            else
            {
                StopAllCoroutines();
                this.textComponent.text = this.currentLine;
            }
        }
    }

    private void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    /** Print the dialogue lines. */
    private IEnumerator TypeLine()
    {
        this.currentLine = this.story.Continue();
        // Type each character 1 by 1.
        foreach (char c in currentLine.ToCharArray())
        {
            this.textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /** Get the next dialogue line. */
    private void NextLine()
    {
        // Run the next dialogue line if there are still more to the dialogue.
        if (this.story.canContinue)
        {
            this.textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

            // Show all choices if available.
            if (this.story.currentChoices.Count != 0)
            {
                this.isMakingChoice = true;
                StartCoroutine(ShowChoices());
            }
        }
        // Close dialogue when no more lines.
        else
        {
            gameObject.SetActive(false);
        }
    }

    /** Instantiate all available choices as a button prefab. */
    private IEnumerator ShowChoices()
    {
        foreach (Choice choice in this.story.currentChoices)
        {
            GameObject newChoice = Instantiate(choicesPrefab, this.choicesPanel.transform);
            newChoice.GetComponent<Button>().onClick.AddListener(delegate { Decision(choice); });
            newChoice.GetComponentInChildren<Text>().text = choice.text;
        }
        yield return new WaitUntil(() => { return this.choiceSelected != null; });
    }

    /** Method for player's decision. */
    private void Decision(Choice choice)
    {
        this.story.ChooseChoiceIndex(choice.index);
        StopAllCoroutines();
        ResetDecision();
        NextLine();
    }

    /** Destroy all choices once a decision is made. */
    private void ResetDecision()
    {
        for (int i = 0; i < this.choicesPanel.transform.childCount; i++)
        {
            Destroy(this.choicesPanel.transform.GetChild(i).gameObject);
        }
        this.choiceSelected = null;
        this.isMakingChoice = false;
    }
}