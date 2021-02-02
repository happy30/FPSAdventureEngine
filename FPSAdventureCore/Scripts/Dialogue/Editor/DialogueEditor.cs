﻿using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    Dialogue dia;
    int value;
    private bool _directDialogue;
    
    

    

    public void OnEnable()
    {
        dia = (Dialogue)target;
    }

    public override void OnInspectorGUI()
    {
        EditorStyles.textArea.wordWrap = true;
        EditorStyles.textField.wordWrap = true;
        GUI.backgroundColor = dia.Color;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(dia.DialogueName, EditorStyles.boldLabel);
        dia.Color = EditorGUILayout.ColorField(dia.Color, GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Dialogue Nr: ", GUILayout.Width(100));
        GUI.backgroundColor = Color.white;
        dia.DialogueName = GUILayout.TextField(dia.DialogueName, GUILayout.Width(40));


        



        if (GUILayout.Button("Update", GUILayout.Width(80)))
        {
            var path = AssetDatabase.GetAssetPath(dia);
            var flds = path.Split('/');
            var nm = flds[flds.Length - 2];
        
        
        
            string tx = "";
            if (dia.Lines.Count > 0)
            {
                tx = dia.Lines[0].Text.Substring(0, dia.Lines[0].Text.Length > 29? 30 : dia.Lines[0].Text.Length);
                tx = tx.Replace('?', ' ');
            }

            string fileName = nm + "_" + dia.DialogueName + "_" + tx;
            
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(dia), fileName);
            Debug.Log(fileName);
            Debug.Log(AssetDatabase.GetAssetPath(dia));
        }
            
        
                GUILayout.Space(Screen.width - 370);
                GUI.backgroundColor = dia.Color;

                EditorGUILayout.EndHorizontal();

                dia.GetCharacters();
                
                if (dia.Lines != null)
                {
                    for (int i = 0; i < dia.Lines.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal("Box");

                        EditorGUILayout.BeginVertical(GUILayout.Width(100));
                        GUI.backgroundColor = dia.Color;
                        dia.Lines[i].SpeakerNumber = EditorGUILayout.IntPopup(dia.Lines[i].SpeakerNumber, dia.GetCharacterNames(), dia.getIntValues(), GUILayout.Width(80));
                        dia.Lines[i].Speaker = dia.Speakers.Speakers[dia.Lines[i].SpeakerNumber];

                        if (dia.Lines[i].VoiceLine == null)
                        {
                            GUI.backgroundColor = Color.cyan;
                        }

                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.BeginDisabledGroup(dia.Lines[i].VoiceLine == null);
                        
                        if(GUILayout.Button(">", GUILayout.Width(20)))
                        {
                            PublicAudioUtil.PlayClip(dia.Lines[i].VoiceLine);
                        }
                        
                        EditorGUI.EndDisabledGroup();
                        
                        dia.Lines[i].VoiceLine =
                            (AudioClip) EditorGUILayout.ObjectField(dia.Lines[i].VoiceLine, typeof(AudioClip), GUILayout.Width(76));

                        GUI.backgroundColor = dia.Color;
                        
                        EditorGUILayout.EndHorizontal();
                        
                        dia.Lines[i].Event = (SceneEvent)EditorGUILayout.ObjectField(dia.Lines[i].Event, typeof(SceneEvent), GUILayout.Width(100));
                        
                        
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Automatic", GUILayout.Width(60));
                        dia.Lines[i].AutomaticLine = EditorGUILayout.Toggle(dia.Lines[i].AutomaticLine);
                        GUILayout.EndHorizontal();
                        if (dia.Lines[i].AutomaticLine)
                        {
                            EditorGUILayout.BeginHorizontal();
                            
                            dia.Lines[i].TimeToNextLine = EditorGUILayout.FloatField(dia.Lines[i].TimeToNextLine, GUILayout.Width(60));
                        
                            EditorGUILayout.EndHorizontal();
                        }
                        
                        
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;
                        dia.Lines[i].Text = EditorGUILayout.TextArea(dia.Lines[i].Text, GUILayout.Height(45), GUILayout.Width(Screen.width - 180));
                        
                        GUI.backgroundColor = dia.Color;

                        if(i>0)
                        {
                            if (GUILayout.Button("-", GUILayout.Width(20)) && i > 0)
                            {
                                dia.Lines.Remove(dia.Lines[i]);
                            }
                        }
                        else
                        {
                            GUILayout.Space(24);
                        }


                        
                        
                        EditorGUILayout.EndHorizontal();

                        
                        

                    }
                }




                if(dia.Choices != null)
                {
                    for(int i = 0; i < dia.Choices.Count; i++)
                    {
                        //string[] names = new string[diaCol.Dialogues.Count];

                        GUILayout.BeginHorizontal("Box");
                        //GUILayout.FlexibleSpace();
                        GUILayout.Label("Choice " + (i + 1).ToString(), GUILayout.Width(80));
                        
                        dia.Choices[i].ChoiceText = EditorGUILayout.TextField(dia.Choices[i].ChoiceText, GUILayout.Width(Screen.width - 343));

                        if (dia.Choices[i].NextDialogue == null)
                        {
                            GUI.backgroundColor = Color.cyan;
                        }
                        dia.Choices[i].NextDialogue =
                            (Dialogue) EditorGUILayout.ObjectField(dia.Choices[i].NextDialogue, typeof(Dialogue),
                                GUILayout.Width(100));
                        
                        GUI.backgroundColor = dia.Color;
                        
                        
                        if (dia.Choices[i].NextDialogue != null)
                        {
                            string[] nextDialogueTexts = new string[dia.Choices[i].NextDialogue.Lines.Count];
                            int[] optValues = new int[dia.Choices[i].NextDialogue.Lines.Count];
                            
                            for (int j = 0; j < dia.Choices[i].NextDialogue.Lines.Count; j++)
                            {
                                nextDialogueTexts[j] = dia.Choices[i].NextDialogue.Lines[j].Text;
                                optValues[j] = value;
                                value++;
                            }
                            
                            value = 0;


                            GUI.backgroundColor = Color.white;
                            

                            GUI.backgroundColor = dia.Choices[i].NextDialogue.Color;

                            
                            dia.Choices[i].NextIndexInDialogue = EditorGUILayout.IntPopup(dia.Choices[i].NextIndexInDialogue, nextDialogueTexts, optValues, GUILayout.Width(100));

                       
                            
                            

                        }
                        GUILayout.EndHorizontal();

                        
                        
                        //int[] optValues = new int[diaCol.Dialogues.Count];
                        
                        
                    }

                    //dia.choices[i].nextDialogue = EditorGUILayout.TextField(dia.choices[i].nextDialogue, GUILayout.Width(100));

                    GUI.backgroundColor = dia.Color;

                    if(dia.Choices.Count > 0)
                    {
                        if (GUILayout.Button("Remove Choices", GUILayout.Width(130)))
                        {
                            dia.Choices.Clear();
                        }
                    }

                }

                if (dia.Variable.Enabled)
                {
                    GUILayout.BeginVertical("Box", GUILayout.Width(400));
                    GUILayout.BeginHorizontal();
                    GUI.backgroundColor = dia.Color;
                    EditorGUILayout.LabelField("If ", GUILayout.Width(80));
                    
                    
                    
                    dia.Variable.BoolVariable =
                        (BoolWithEvent) EditorGUILayout.ObjectField(dia.Variable.BoolVariable, typeof(BoolWithEvent), true, GUILayout.Width(200));
                    
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("True: ", GUILayout.Width(80));

                    if (dia.Variable.NextDialogueTrue == null)
                    {
                        GUI.backgroundColor = Color.cyan;
                    }
                    dia.Variable.NextDialogueTrue = (Dialogue)EditorGUILayout.ObjectField(dia.Variable.NextDialogueTrue, typeof(Dialogue), true, GUILayout.Width(200));

                    GUI.backgroundColor = dia.Color;
                    
                    if (dia.Variable.NextDialogueTrue != null)
                    {
                        string[] nextDialogueTextsTrue = new string[dia.Variable.NextDialogueTrue.Lines.Count];
                        int[] optValues2 = new int[dia.Variable.NextDialogueTrue.Lines.Count];
                        
                        for (int j = 0; j < dia.Variable.NextDialogueTrue.Lines.Count; j++)
                        {
                            nextDialogueTextsTrue[j] = dia.Variable.NextDialogueTrue.Lines[j].Text;
                            optValues2[j] = value;
                            value++;
                        }
                        
                        
                        dia.Variable.NextIndexInDialogueTrue = EditorGUILayout.IntPopup(
                            dia.Variable.NextIndexInDialogueTrue, nextDialogueTextsTrue, optValues2, GUILayout.Width(100));
                        
                    }

                    value = 0;
                    
                    EditorGUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("False: ", GUILayout.Width(80));

                    if (dia.Variable.NextDialogueFalse == null)
                    {
                        GUI.backgroundColor = Color.cyan;
                    }
                    
                    dia.Variable.NextDialogueFalse = (Dialogue)EditorGUILayout.ObjectField(dia.Variable.NextDialogueFalse, typeof(Dialogue), true, GUILayout.Width(200));

                    GUI.backgroundColor = dia.Color;
                    
                    if (dia.Variable.NextDialogueFalse != null)
                    {
                        string[] nextDialogueTextsFalse = new string[dia.Variable.NextDialogueFalse.Lines.Count];
                        int[] optValues3 = new int[dia.Variable.NextDialogueFalse.Lines.Count];
                        
                        for (int j = 0; j < dia.Variable.NextDialogueFalse.Lines.Count; j++)
                        {
                            nextDialogueTextsFalse[j] = dia.Variable.NextDialogueFalse.Lines[j].Text;
                            optValues3[j] = value;
                            value++;
                        }
                        
                        dia.Variable.NextIndexInDialogueFalse = EditorGUILayout.IntPopup(
                            dia.Variable.NextIndexInDialogueFalse, nextDialogueTextsFalse, optValues3, GUILayout.Width(100));
                    }

                    value = 0;
                    
                    GUILayout.EndHorizontal();
                    
                    
                    GUI.backgroundColor = dia.Color;
                    
                    
                    if (GUILayout.Button("Remove Condition", GUILayout.Width(130)))
                    {
                        dia.Variable.BoolVariable = null;
                        dia.Variable.Enabled = false;
                    }
                    
                    GUILayout.EndVertical();
                    
                }
        
        
         

        if (dia.DirectTo.Enabled)
        {
            GUILayout.BeginVertical("Box", GUILayout.Width(400));
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Next Dialogue:", GUILayout.Width(90));

            if (dia.DirectTo.NewDialogue == null)
            {
                GUI.backgroundColor = Color.cyan;
            }

            
            dia.DirectTo.NewDialogue =
                (Dialogue) EditorGUILayout.ObjectField(dia.DirectTo.NewDialogue, typeof(Dialogue), true,
                    GUILayout.Width(180));
            GUI.backgroundColor = dia.Color;
            
            if (dia.DirectTo.NewDialogue != null)
            {
                string[] nextDialogueTexts= new string[dia.DirectTo.NewDialogue.Lines.Count];
                int[] optValues = new int[dia.DirectTo.NewDialogue.Lines.Count];
                        
                for (int j = 0; j < dia.DirectTo.NewDialogue.Lines.Count; j++)
                {
                    nextDialogueTexts[j] = dia.DirectTo.NewDialogue.Lines[j].Text;
                    optValues[j] = value;
                    value++;
                }
                        
                        
                dia.DirectTo.NewDialogueIndex = EditorGUILayout.IntPopup(
                    dia.DirectTo.NewDialogueIndex, nextDialogueTexts, optValues, GUILayout.Width(100));
                        
            }

            value = 0;
            
            
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                dia.DirectTo.NewDialogue = null;
                dia.DirectTo.Enabled = false;
            }

            GUILayout.EndVertical();
        }
        
        
        
        
        

                GUILayout.BeginHorizontal();

                if(dia.Choices.Count == 0 && !dia.Variable.Enabled && !dia.DirectTo.Enabled)
                {
                    if (GUILayout.Button("New Line", GUILayout.Width(100)))
                    {
                        dia.Lines.Add(new Line());
                    }

                    if(dia.Lines.Count > 0)
                    {
                        if (GUILayout.Button("Add Choice", GUILayout.Width(100)))
                        {
                            dia.Choices.Add(new Choice());
                            dia.Choices.Add(new Choice());
                            dia.Choices.Add(new Choice());
                        }
                    }

                    if (dia.Lines.Count > 0)
                    {
                        if (GUILayout.Button("Add Condition", GUILayout.Width(100)))
                        {
                            dia.Variable.Enabled = true;
                        }
                        
                        if(GUILayout.Button("Direct to...", GUILayout.Width(100)))
                        {
                            dia.DirectTo.Enabled = true;
                        }

                    }
                    
                    
                }

                GUILayout.EndHorizontal();
        
                
        
                GUILayout.Space(30);

            
        EditorUtility.SetDirty(dia);
        }

        
    }
    


