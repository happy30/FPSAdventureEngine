using System.Collections;
using System.Collections.Generic;
using EventObjects;
using TMPro;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    private int currentText;
    private Dialogue currentDialogue;
    private int currentChar;

    public TextMeshProUGUI ConversantText;
    public TextMeshProUGUI ConversantSpeaker;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI PlayerSpeaker;

    private string _conversantDisplayText;
    private string _playerDisplayText;

    public TextMeshProUGUI Choice1;
    public TextMeshProUGUI Choice2;
    public TextMeshProUGUI Choice3;

    public GameObject Choice1GO;
    public GameObject Choice2GO;
    public GameObject Choice3GO;

    public DialogeSpeakers Speakers;

    public float textDelay;
    private bool _textDone;

    public BoolWithEvent LineCompleted;
    public DialogueWithEvent DialogueWithEvent;

    public AudioSource VoiceLineSourceConversant;
    public AudioSource VoiceLineSourcePlayer;

    public Dialogue DebugDialogue;

    public BoolWithEvent SkipDialogue;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (DebugDialogue != null)
        {
            DialogueWithEvent.Init();
            DialogueWithEvent.Value = DebugDialogue;
            currentDialogue = DebugDialogue;
        }
        
        
        else if (DialogueWithEvent.Value != null)
        {
            currentDialogue = DialogueWithEvent.Value;
        }

        StartCoroutine(StartAfterDelay());

        if (AudioListener.volume < 1f)
        {
            StartCoroutine(FadeInVolume());
        }

    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(2);
        ResetTexts();
        DoDialogue();
    }

    IEnumerator FadeInVolume()
    {
        while (AudioListener.volume < 1)
        {
            AudioListener.volume += Time.deltaTime * 0.25f;
            yield return null;
        }
    }
    
    public void DoDialogue()
    {
        Dialogue dia = currentDialogue;

        //Check if there's no lines left in dialogue
        if (dia.Lines.Count < currentText + 1)
        {
            //Check if we should show choices
            if (dia.Choices.Count > 0)
            {
                ShowChoices(dia);
            }
            //Check if the dialogue should be directed by variable
            else if(dia.Variable.BoolVariable != null)
            {
                CheckBoolAndDirectToNewDialogue(dia);
            }
            else if (dia.DirectTo.NewDialogue != null)
            {
                currentText = dia.DirectTo.NewDialogueIndex;
                currentDialogue = dia.DirectTo.NewDialogue;
                //ResetTexts();
                DoDialogue();
            }
        }
        else
        {
            //Show dialogue
            DisplayDialogue(dia);
        }
    }

    private void DisplayDialogue(Dialogue dia)
    {
        //Set up new lines and then send them to the IEnumerator for typewriter effect
        Line line = dia.Lines[currentText];
        if (line.Speaker == Speakers.Speakers[0] || line.Speaker == Speakers.Speakers[2] || line.Speaker == Speakers.Speakers[3] ) //Amily or ??? or Caitlin
        {
            ConversantSpeaker.text =  line.Speaker;
            StartCoroutine(FillLine(line.Text, ConversantText, textDelay, !line.AutomaticLine && !line.VoiceLine));

            if (line.VoiceLine)
            {
                StartCoroutine(SetLineCompletedByVoice(line.VoiceLine.length));
            }
        }
        else if (line.Speaker == Speakers.Speakers[1]) //Derek
        {
            PlayerSpeaker.text = line.Speaker;
            StartCoroutine(FillLine(line.Text, PlayerText, textDelay, !line.AutomaticLine&& !line.VoiceLine));
            if (line.VoiceLine)
            {
                StartCoroutine(SetLineCompletedByVoice(line.VoiceLine.length));
            }
        }

        if (line.AutomaticLine)
        {
            StartCoroutine(NextLineAfterDelay(line.TimeToNextLine));
        }
        

        //Check if theres a voice line to be played
        if (line.VoiceLine != null)
        {
            if(line.Speaker == Speakers.Speakers[0]) //Amily
            {
                //VoiceLineSourceConversant.clip = line.VoiceLine;
                //VoiceLineSourceConversant.Play();
                VoiceLineSourceConversant.PlayOneShot(line.VoiceLine, 1);
            }
            else
            {
                //VoiceLineSourcePlayer.clip = line.VoiceLine;
                //VoiceLineSourcePlayer.Play();
                VoiceLineSourcePlayer.PlayOneShot(line.VoiceLine, 1);
            }
            
            
        }

        //Check if there's an event to raise
        if (line.Event != null)
        {
            line.Event.Raise();
        }
    }

    private void CheckBoolAndDirectToNewDialogue(Dialogue dia)
    {
        if (dia.Variable.BoolVariable.Value)
        {
            currentDialogue = dia.Variable.NextDialogueTrue;
            currentText = dia.Variable.NextIndexInDialogueTrue;
        }
        else
        {
            currentDialogue = dia.Variable.NextDialogueFalse;
            currentText = dia.Variable.NextIndexInDialogueFalse;
        }

        //ResetTexts();
        DoDialogue();
    }
    
    public void DirectDialogueTo(int choice)
    {
        Dialogue dia = currentDialogue.Choices[choice].NextDialogue;
        currentText = currentDialogue.Choices[choice].NextIndexInDialogue;
        currentDialogue = dia;
        

        Choice1GO.SetActive(false);
        Choice2GO.SetActive(false);
        Choice3GO.SetActive(false);
        
        //VoiceLineSourceConversant.Stop();
        //VoiceLineSourcePlayer.Stop();
        
        //ResetTexts();
        DoDialogue();
    }

    private void ShowChoices(Dialogue dia)
    {
        PlayerText.text = "";;
        PlayerSpeaker.text = "";
        Choice1GO.SetActive(true);
        Choice2GO.SetActive(true);
        Choice3GO.SetActive(true);

        EnableChoices(dia);
    }

    private void EnableChoices(Dialogue dia)
    {
        Choice1GO.SetActive(true);
        Choice2GO.SetActive(true);
        Choice3GO.SetActive(true);
        
        StartCoroutine(FillChoiceLine(dia.Choices[0].ChoiceText, Choice1, textDelay));
        StartCoroutine(FillChoiceLine(dia.Choices[1].ChoiceText, Choice2, textDelay));
        StartCoroutine(FillChoiceLine(dia.Choices[2].ChoiceText, Choice3, textDelay));

    }

    private void ResetTexts()
    {
        Choice1GO.SetActive(false);
        Choice2GO.SetActive(false);
        Choice3GO.SetActive(false);
        
        ConversantText.text = "";
        ConversantSpeaker.text = "";
        PlayerText.text = "";
        PlayerSpeaker.text = "";
        
        //VoiceLineSourceConversant.Stop();
        //VoiceLineSourcePlayer.Stop();
    }

    //Fill in the choices typewriter style
    IEnumerator FillChoiceLine(string fullLine, TextMeshProUGUI displayLine, float textSpeed)
    {
        LineCompleted.SetValue(false);
        displayLine.text = "";
        for (int i = 0; i < fullLine.Length; i++)
        {
            displayLine.text += fullLine[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //Fill in the Text typewriter style
    IEnumerator FillLine(string fullLine, TextMeshProUGUI displayLine, float textSpeed, bool SetLineCompleted)
    {
        if (!SkipDialogue.Value)
        {
            LineCompleted.SetValue(false);
            displayLine.text = "";
            for (int i = 0; i < fullLine.Length; i++)
            {
                displayLine.text += fullLine[i];
                yield return new WaitForSeconds(textSpeed);
            }

            currentText++;
            LineCompleted.SetValue(SetLineCompleted);
        }
        else
        {
            displayLine.text = fullLine;
            currentText++;
            LineCompleted.SetValue(SetLineCompleted);
            
        }
        
        
    }

    IEnumerator SetLineCompletedByVoice(float time)
    {
        if (!SkipDialogue.Value)
        {
            yield return new WaitForSeconds(time);
        }
        
        LineCompleted.SetValue(true);
    }
    

    IEnumerator NextLineAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DoDialogue();
    }
}
