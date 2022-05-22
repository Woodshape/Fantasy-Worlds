using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World 
{
    private static World instance;

    public List<Instance> Instances = new List<Instance>();
    public Dictionary<string, Thing> Things = new Dictionary<string, Thing>();
    public Dictionary<string, Name> Names = new Dictionary<string, Name>();

    [SerializeField]
    public int NumberOfThings = 0;
    [SerializeField]
    public int NumberOfNames = 0;
    [SerializeField]
    public int NumberOfInstances = 0;

    private string startingThing;
    private Thing[] things;
    private Name[] names;

    private World() {}

    public static World get(){
        if (instance == null)
        {
            instance = new World(); 
        }

        return instance;
    }

    public static void Generate(){
        foreach (var name in get().names)
        {
            AddName(name);
        }

       Debug.Log($"Registered {get().NumberOfNames} Names");

        foreach (var thing in get().things)
        {
            AddThing(thing);
        }

       Debug.Log($"Created {get().NumberOfThings} Things");

        new Instance(GetThing(get().startingThing));

        Debug.Log($"Generated {get().NumberOfInstances} Instances");
    }

    public static void FromThings(Thing[] things){
        get().things = things;
    }

    public static void FromNames(Name[] names){
        get().names = names;
    }

    public static void FromStartingThing(string thing){
        get().startingThing = thing;
    }

    public static void AddThing(Thing thing){
        get().Things.TryAdd(thing.Name, thing);
        get().NumberOfThings++;
    }

    public static void AddName(Name name){
        get().Names.TryAdd(name.Type, name);
        get().NumberOfNames++;
    }

    public static void AddInstance(Instance instance){
        get().Instances.Add(instance);
        get().NumberOfInstances++;
    }

    public static Thing GetThing(string thing){
        get().Things.TryGetValue(thing, out Thing realThing);
        return realThing;
    }

    public static bool HasThing(string thing){
        return get().Things.ContainsKey(thing);
    }
}
