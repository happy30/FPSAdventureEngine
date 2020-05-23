using System;
using UnityEngine;
using UnityEngine.Events;

namespace EventObjects
{
    
    [Serializable]
    [CreateAssetMenu(menuName = "EventObjects/Special/InspectModeComponent", fileName = "New InspectModeComponent")]
    public class InspectModeComponentWithEvent : ValueWithEvent<InspectModeComponent, InspectModeComponentEvent>, IConditional
    {
        public bool Resolve()
        {
            return true; 
        }
    }

    [Serializable]
    public class InspectModeComponentVariable : UnityEvent<InspectModeComponent>{}
}