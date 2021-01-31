using System;
using UnityEngine;

namespace EventObjects
{
    [CreateAssetMenu(menuName = "EventObjects/String", fileName = "New StringArrayEvent")]
    public class StringArrayWithEvent : ValueWithEvent<string[], StringArrayEvent>, IConditional
    {
        public bool Resolve()
        {
            return true; 
        }
    }

    [Serializable]
    public class StringArrayVariable : CachedVariable<StringArrayWithEvent, string[], StringArrayEvent> {}
}