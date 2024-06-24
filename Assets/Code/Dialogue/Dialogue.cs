using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
	[SerializeField] public string npcName;
	[SerializeField] public string questDescription;
    [SerializeField] public bool questGiven = false;
    [SerializeField] public string[] sentences;
}
