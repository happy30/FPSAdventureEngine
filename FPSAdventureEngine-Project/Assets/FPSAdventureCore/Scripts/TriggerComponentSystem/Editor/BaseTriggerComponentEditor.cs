using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(BaseTriggerComponent))]
public class BaseTriggerComponentEditor : Editor
{
    private BaseTriggerComponent _baseTriggerComponent;

    
    
    
    private void OnEnable()
    {
        _baseTriggerComponent = (BaseTriggerComponent) target;
        
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GUILayout.Space(5);
        
        #region SoundEffect

        GUI.backgroundColor = new Color(0.55f, 1f, 0.57f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Add Sound Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.AudioTriggers.Add(new AudioTrigger());
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;


        for (int i = 0; i < _baseTriggerComponent.AudioTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            
                
            var audioTrigger = _baseTriggerComponent.AudioTriggers[i];
                
            // Audioclip to play
            GUILayout.Label("Clip " + (i+1) + ":", GUILayout.Width(60));
                
            audioTrigger.Clip =
                (AudioClip) EditorGUILayout.ObjectField(audioTrigger.Clip, typeof(AudioClip), GUILayout.Width(100));
            
            //Audio volume of clip
            GUILayout.Label("Volume: ", GUILayout.Width(60));
            audioTrigger.Volume = EditorGUILayout.Slider(audioTrigger.Volume, 0f, 100f);
            
            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.AudioTriggers.Remove(audioTrigger);
            }
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            //Audiosource that plays the clip
            GUILayout.Label("Source: ", GUILayout.Width(60));
            
            audioTrigger.Source =
                (AudioSource) EditorGUILayout.ObjectField(audioTrigger.Source, typeof(AudioSource), true, GUILayout.Width(100));
            
            //Play sound after a delay
            GUILayout.Label("Delay: ", GUILayout.Width(60));
            
            audioTrigger.Delay =
                EditorGUILayout.FloatField(audioTrigger.Delay, GUILayout.Width(80));
    
                
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            //Loop the audio?
            GUILayout.Label("Loop: ", GUILayout.Width(60));
            audioTrigger.Loop = EditorGUILayout.Toggle(audioTrigger.Loop);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        #endregion
       
        #region SceneObject
        GUI.backgroundColor = new Color(1f, 0.65f, 0.59f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add SceneObject Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.SceneObjectTriggers.Add(new SceneObjectTrigger());
        }

        GUI.backgroundColor = Color.white;
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _baseTriggerComponent.SceneObjectTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var sceneObjectTrigger = _baseTriggerComponent.SceneObjectTriggers[i];

            //Which object to change state
            GUILayout.Label("Scene Object " + (i + 1) + ":", GUILayout.Width(100));
            sceneObjectTrigger.SceneObject =
                (GameObject) EditorGUILayout.ObjectField(sceneObjectTrigger.SceneObject, typeof(GameObject),
                    true, GUILayout.Width(200));
            
            //To which state
            string[] _choices = { "To Active", "To Inactive" };
            var _choiceIndex = sceneObjectTrigger.SetObjectActive? 0 : 1;
            
            sceneObjectTrigger.SetObjectActive = EditorGUILayout.Popup(_choiceIndex, _choices, GUILayout.Width(100)) == 0;
             
            
            //Remove option
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.SceneObjectTriggers.Remove(sceneObjectTrigger);
            }
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            //Revert back after duration?
            GUILayout.Label("Revert back: ", GUILayout.Width(100));
            sceneObjectTrigger.ToInitialStateAfterDuration =
                EditorGUILayout.Toggle(sceneObjectTrigger.ToInitialStateAfterDuration, GUILayout.Width(20));

            //Duration before reverting
            EditorGUI.BeginDisabledGroup(!sceneObjectTrigger.ToInitialStateAfterDuration);
            sceneObjectTrigger.Duration =
                EditorGUILayout.FloatField(sceneObjectTrigger.Duration, GUILayout.Width(80));
            EditorGUI.EndDisabledGroup();
            
            //Event happening after x seconds
            GUILayout.Label("Delay: ", GUILayout.Width(60));
                
            sceneObjectTrigger.Delay =
                EditorGUILayout.FloatField(sceneObjectTrigger.Delay, GUILayout.Width(80));
            
            
            GUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        
        #endregion
        
        #region Teleport
        
        GUI.backgroundColor = new Color(0.77f, 0.78f, 1f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Teleport Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.TeleportTriggers.Add(new TeleportTrigger());
        }
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;

        for (int i = 0; i < _baseTriggerComponent.TeleportTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var teleportTrigger = _baseTriggerComponent.TeleportTriggers[i];
            
            //What transform the teleport
            GUILayout.Label("Transform " + (i + 1) + ":", GUILayout.Width(100));
            teleportTrigger.TeleportedObject =
                (Transform) EditorGUILayout.ObjectField(teleportTrigger.TeleportedObject, typeof(Transform),
                    GUILayout.Width(200));
            
            //Change rotation of transform to new rotation
            GUILayout.Label("Change Rotation: ", GUILayout.Width(100));
            teleportTrigger.CopyRotation =
                EditorGUILayout.Toggle(teleportTrigger.CopyRotation, GUILayout.Width(20));
            
            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.TeleportTriggers.Remove(teleportTrigger);
            }
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            
            
            string[] _choices = { "Absolute", "Relative" };
            var _choiceIndex = teleportTrigger.Relative? 0 : 1;
            
            teleportTrigger.Relative = EditorGUILayout.Popup(_choiceIndex, _choices, GUILayout.Width(100)) == 1;
            
            
            //Transform location to teleport to
            if (teleportTrigger.Relative)
            {
                teleportTrigger.RelativeLocation = EditorGUILayout.Vector3Field("",
                    teleportTrigger.RelativeLocation, GUILayout.Width(200));


            }
            else
            {
                
                teleportTrigger.TargetLocation =
                    (Transform) EditorGUILayout.ObjectField(teleportTrigger.TargetLocation, typeof(Transform),
                        GUILayout.Width(200));
            }
            
            
            
            //Teleport after a delay
            GUILayout.Label("Delay: ", GUILayout.Width(60));
                
            teleportTrigger.Delay =
                EditorGUILayout.FloatField(teleportTrigger.Delay, GUILayout.Width(80));
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

        }

        GUILayout.EndVertical();

        #endregion
        
        #region Material
        
        
        GUI.backgroundColor = new Color(0.55f, 1f, 0.84f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Material Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.MaterialTriggers.Add(new ModifyMaterialTrigger());
        }
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;

        for (int i = 0; i < _baseTriggerComponent.MaterialTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var materialTrigger = _baseTriggerComponent.MaterialTriggers[i];

            //What renderer in the scene to change
            GUILayout.Label("Renderer " + (i + 1) + ":", GUILayout.Width(100));
            materialTrigger.Renderer =
                (Renderer) EditorGUILayout.ObjectField(materialTrigger.Renderer, typeof(Renderer),
                    true, GUILayout.Width(200));


            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.MaterialTriggers.Remove(materialTrigger);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            materialTrigger.NewAlbedo = TextureField("Albedo", materialTrigger.NewAlbedo);
            materialTrigger.NewMetallic = TextureField("Metallic", materialTrigger.NewMetallic);
            materialTrigger.NewNormal = TextureField("Normal", materialTrigger.NewNormal);
            
            

            
            GUILayout.BeginVertical();
            GUILayout.Space(20);

            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Albedo Color:", GUILayout.Width(100));
            materialTrigger.AlbedoColor = EditorGUILayout.ColorField(materialTrigger.AlbedoColor, GUILayout.Width(40));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();

            GUILayout.Label("Emission Color:", GUILayout.Width(100));
            materialTrigger.EmissionColor = EditorGUILayout.ColorField(materialTrigger.EmissionColor, GUILayout.Width(40));

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Delay: ", GUILayout.Width(60));
                
            materialTrigger.Delay =
                EditorGUILayout.FloatField(materialTrigger.Delay, GUILayout.Width(80));
            GUILayout.EndHorizontal();
            
            
            GUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            
            GUILayout.EndVertical();
        }

        GUILayout.EndVertical();

        #endregion

        #region Rigidbody

        GUI.backgroundColor = new Color(1f, 0.74f, 0.49f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Rigidbody Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.RigidbodyTriggers.Add(new RigidbodyTrigger());
        }
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;

        for (int i = 0; i < _baseTriggerComponent.RigidbodyTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var rigidbodyTrigger = _baseTriggerComponent.RigidbodyTriggers[i];


            //What rigidbody in the scene to change
            GUILayout.Label("Rigidbody " + (i + 1) + ":", GUILayout.Width(100));
            rigidbodyTrigger.Rigidbody =
                (Rigidbody) EditorGUILayout.ObjectField(rigidbodyTrigger.Rigidbody, typeof(Rigidbody),
                    true, GUILayout.Width(200));


            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.RigidbodyTriggers.Remove(rigidbodyTrigger);
            }

            GUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(rigidbodyTrigger.Rigidbody == null);
            if (GUILayout.Button("Get Standard Parameters"))
            {
                rigidbodyTrigger.Drag = rigidbodyTrigger.Rigidbody.drag;
                rigidbodyTrigger.Mass = rigidbodyTrigger.Rigidbody.mass;
                rigidbodyTrigger.UseGravity = rigidbodyTrigger.Rigidbody.useGravity;
                
                rigidbodyTrigger.PositionConstraints.x =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionX;
                rigidbodyTrigger.PositionConstraints.y =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionY;
                rigidbodyTrigger.PositionConstraints.z =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionZ;
                
                
                rigidbodyTrigger.RotationConstraints.x =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionX;
                rigidbodyTrigger.RotationConstraints.y =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionY;
                rigidbodyTrigger.RotationConstraints.z =
                    rigidbodyTrigger.Rigidbody.constraints == RigidbodyConstraints.FreezePositionZ;
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Mass: ", GUILayout.Width(60));
                
            rigidbodyTrigger.Mass =
                EditorGUILayout.FloatField(rigidbodyTrigger.Mass, GUILayout.Width(80));
            
            GUILayout.Label("Position Constraints: ", GUILayout.Width(130));
            rigidbodyTrigger.PositionConstraints.x = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "X", GUILayout.Width(30));
            rigidbodyTrigger.PositionConstraints.y = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "Y", GUILayout.Width(30));
            rigidbodyTrigger.PositionConstraints.z = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "Z", GUILayout.Width(30));
            
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Drag: ", GUILayout.Width(60));
                
            rigidbodyTrigger.Drag =
                EditorGUILayout.FloatField(rigidbodyTrigger.Drag, GUILayout.Width(80));
            
            GUILayout.Label("Rotation Constraints: ", GUILayout.Width(130));
            rigidbodyTrigger.RotationConstraints.x = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "X", GUILayout.Width(30));
            rigidbodyTrigger.RotationConstraints.y = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "Y", GUILayout.Width(30));
            rigidbodyTrigger.RotationConstraints.z = GUILayout.Toggle(rigidbodyTrigger.PositionConstraints.x, "Z", GUILayout.Width(30));
            
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Delay: ", GUILayout.Width(60));
                
            rigidbodyTrigger.Delay =
                EditorGUILayout.FloatField(rigidbodyTrigger.Delay, GUILayout.Width(80));
            
            
            rigidbodyTrigger.UseGravity = GUILayout.Toggle(rigidbodyTrigger.UseGravity, "Use Gravity", GUILayout.Width(180));
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();

        }

        GUILayout.EndVertical();

        #endregion
        
        #region Animation
        

        GUI.backgroundColor = new Color(1f, 0.72f, 0.95f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Animation Event", GUILayout.Width(250)))
        {
            _baseTriggerComponent.AnimationTriggers.Add(new PlayAnimationTrigger());
        }
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;

        for (int i = 0; i < _baseTriggerComponent.AnimationTriggers.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var animationTrigger = _baseTriggerComponent.AnimationTriggers[i];
            
            //What renderer in the scene to change
            GUILayout.Label("Animator: " + (i + 1) + ":", GUILayout.Width(100));
            animationTrigger.Animator =
                (Animator) EditorGUILayout.ObjectField(animationTrigger.Animator, typeof(Animator),
                    true, GUILayout.Width(200));
            
            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.AnimationTriggers.Remove(animationTrigger);
            }

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("SetTrigger: ", GUILayout.Width(80));
                
            animationTrigger.TriggerString =
                EditorGUILayout.TextField(animationTrigger.TriggerString, GUILayout.Width(80));
            
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("SetBool: ", GUILayout.Width(80));
                
            animationTrigger.Bool =
                EditorGUILayout.TextField(animationTrigger.Bool, GUILayout.Width(80));

            animationTrigger.SetBool = EditorGUILayout.Toggle(animationTrigger.SetBool);
            
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Delay: ", GUILayout.Width(80));
                
            animationTrigger.Delay =
                EditorGUILayout.FloatField(animationTrigger.Delay, GUILayout.Width(80));
            
            GUILayout.EndHorizontal();
            
            
            GUILayout.EndVertical();
            
                

        }
        
        GUILayout.EndVertical();

        #endregion
        
        
        
        #region UnityEvent
        
        
        GUI.backgroundColor = new Color(1f, 0.98f, 0.53f);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(Screen.width -40));


        GUI.backgroundColor = Color.white;

        
        
        if(GUILayout.Button("Add Scene Event", GUILayout.Width(150)))
        {
            _baseTriggerComponent.SceneEventTriggers.Add(new SceneEventTrigger());
        }
        

        for (int i = 0; i < _baseTriggerComponent.SceneEventTriggers.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var sceneEventTrigger = _baseTriggerComponent.SceneEventTriggers[i];
            
            GUILayout.Label("Scene Effect: " + (i + 1) + ":", GUILayout.Width(100));
            sceneEventTrigger.SceneEvent =
                (SceneEvent) EditorGUILayout.ObjectField(sceneEventTrigger.SceneEvent, typeof(SceneEvent),
                    GUILayout.Width(200));
            
            GUILayout.Label("Delay: ", GUILayout.Width(60));
                
            sceneEventTrigger.Delay =
                EditorGUILayout.FloatField(sceneEventTrigger.Delay, GUILayout.Width(80));
            
            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.SceneEventTriggers.Remove(sceneEventTrigger);
            }
            
            
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
        if(GUILayout.Button("Add Change Bool Value", GUILayout.Width(150)))
        {
            _baseTriggerComponent.BoolEvents.Add(new BoolWithEventTrigger());
        }
        

        for (int i = 0; i < _baseTriggerComponent.BoolEvents.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            var boolEvent = _baseTriggerComponent.BoolEvents[i];
            
            GUILayout.Label("Bool Event: " + (i + 1) + ":", GUILayout.Width(100));
            boolEvent.Bool =
                (BoolWithEvent) EditorGUILayout.ObjectField(boolEvent.Bool, typeof(BoolWithEvent), false,
                    GUILayout.Width(200));
            

            

            boolEvent.NewState = EditorGUILayout.Toggle("New State: ", boolEvent.NewState);
            
            //Remove this event
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _baseTriggerComponent.BoolEvents.Remove(boolEvent);
            }
            
            
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
        GUI.backgroundColor = Color.white;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("UnityTriggerEvent"), true);
        serializedObject.ApplyModifiedProperties();
        
        EditorGUILayout.EndVertical();
        #endregion
        
        
        EditorUtility.SetDirty(_baseTriggerComponent);

    }
    
    private static Texture2D TextureField(string name, Texture2D texture)
    {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }



}
