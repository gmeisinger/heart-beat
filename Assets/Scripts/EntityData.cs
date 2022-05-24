using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Entity Data")]
public class EntityData : ScriptableObject
{
	[Serializable]
	public class StateData
	{
		public string key;
		public MonoScript stateType;
	}
    public string entityName;
	public string entityDescription;
	public List<StateData> startingStates = new List<StateData>();
	//skills
	//sprites
	//appearance data
	//attributes, etc
}
