using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Names", fileName = "Name")]
public class Name : ScriptableObject
{
    public string Type;
    public List<string> Options;
}
