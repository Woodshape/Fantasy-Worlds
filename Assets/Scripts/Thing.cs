using UnityEngine;

[CreateAssetMenu(menuName = "Things", fileName = "Thing")]
public class Thing : ScriptableObject
{
    public string Name; 
    public string[] Contains = new string[] {};
    public string NameGen;
    public string DisplayName;

    public override string ToString()
    {
        return $"Thing: {{ {this.Name}; {this.NameGen} }} Contains: {{ {string.Join(", ", this.Contains)} }}";
    }
}
