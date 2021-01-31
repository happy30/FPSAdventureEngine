using System;
using UnityEngine;
using UnityEngine.Events;

namespace EventObjects
{
    
    [Serializable]
    [CreateAssetMenu(menuName = "EventObjects/Special/InspectModeInteractionPoint", fileName = "New InspectModeInteractionPoint")]
    public class InspectModeInteractionPointWithEvent : ValueWithEvent<InspectModeInteractionPoint, InspectModeInteractionPointEvent>, IConditional
    {
        public bool Resolve()
        {
            return true; 
        }
    }

    [Serializable]
    public class InspectModeInteractionPointVariable : UnityEvent<InspectModeInteractionPoint>{}
}