using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatModifierSO : MonoBehaviour
{
	public abstract void AffectCharacter(GameObject character, float val);
}
