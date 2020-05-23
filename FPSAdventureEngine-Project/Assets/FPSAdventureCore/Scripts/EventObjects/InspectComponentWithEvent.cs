using System;
using UnityEngine;
using UnityEngine.Events;

namespace EventObjects
{
    
    [Serializable]
    [CreateAssetMenu(menuName = "EventObjects/Special/InspectComponent", fileName = "New InspectComponent")]
    public class InspectComponentWithEvent : ValueWithEvent<InspectComponent, InspectComponentEvent>, IConditional
    {
        public bool Resolve()
        {
            return true; 
        }
    }

    [Serializable]
    public class InspectComponentVariable : UnityEvent<InspectComponent>{}
}
