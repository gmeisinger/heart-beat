using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    protected string skillName;
	protected string skillDescription;
	protected float activationTime;
	protected float recoveryTime;
	protected float cooldownTime;
}
