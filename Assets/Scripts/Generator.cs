using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public string StartingThing;

    [SerializeField]
    private string thingsResourcesFolder = default;
    [SerializeField]
    private string namesResourceFolder = default;

    void Start()
    {
        Thing[] things = Resources.LoadAll<Thing>(thingsResourcesFolder);
        Name[] names = Resources.LoadAll<Name>(namesResourceFolder);

        World.FromThings(things);
        World.FromNames(names);
        World.FromStartingThing(StartingThing);
        World.Generate();
    }
}
