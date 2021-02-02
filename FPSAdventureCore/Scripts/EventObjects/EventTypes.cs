using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTypes : MonoBehaviour
{

}

[Serializable] public class BoolEvent : UnityEvent<bool>{}
[Serializable] public class IntEvent : UnityEvent<int>{}
[Serializable] public class FloatEvent : UnityEvent<float>{}
[Serializable] public class SpriteEvent : UnityEvent<Sprite>{}
[Serializable] public class StringArrayEvent : UnityEvent<string[]>{}

[Serializable] public class GameObjectEvent : UnityEvent<GameObject>{}  

