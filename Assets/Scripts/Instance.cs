using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance 
{
    public string Name { get; set; }
    public Thing Type { get; set; }
    public List<Instance> Children { get; set; } = new List<Instance>();

    public Instance(Thing what)
    {
        if (what == null)
        {
            Debug.LogError("No Thing to instantiate!");   
            return;
        }
        
        this.Name = DetermineName(what);
        this.Type = what;    

        Debug.Log("Created: " + this);

        World.AddInstance(this);

        Grow();
    }

    public void Grow(){
        foreach (string contained in Type.Contains)
        {
            ThingInstance toCreate = ParseThing(contained);
            int probability = Random.Range(0, 101);
            if (probability > toCreate.Percentage)
            {
                continue;
            }

            if (World.HasThing(toCreate.Thing))
            {
                for (int i = 0; i < toCreate.Amount; i++)
                {
                    Debug.Log("Growing: " + toCreate.Thing);
                    Instance instance = new Instance(World.GetThing(toCreate.Thing));
                    Children.Add(instance);
                    CreatePrefab();
                }
            }else{
                Debug.LogWarning("Trying to grow unknown Thing: " + toCreate.Thing);
            }
        }
    }

    private ThingInstance ParseThing(string toParse){
        ThingInstance instance = new ThingInstance();
        string thing = toParse;
        int amount = 1;
        int percentage = 100;

        string[] subThing = toParse.Split(",");

        if (subThing.Length != 1)
        {
            thing = subThing[0];

            if (subThing[1].Contains("-"))
            {
                string[] subAmount = subThing[1].Split("-");
                if (subAmount.Length > 0)
                {
                    int.TryParse(subAmount[0], out int min);
                    int.TryParse(subAmount[1], out int max);
                    amount = Random.Range(min, max + 1);
                }
            }else if (subThing[1].Contains("%"))
            {
                string[] subPercent = subThing[1].Split("%");
                if (subPercent.Length > 0)
                {
                    int.TryParse(subPercent[0], out int percent);
                    percentage = percent;
                }
            }
        }

        instance.Thing = thing;
        instance.Amount = amount;
        instance.Percentage = percentage;

        return instance;
    }

    private string DetermineName(Thing thing){
        string name = thing.Name;

        if (thing.DisplayName != "")
        {
            name = thing.DisplayName;   
        }else{
            switch (thing.NameGen)
            {
                case "Continent":
                    name = "JOJOJOJO";
                    break;
                default:
                break; 
            }
        }

        return name;
    }

    private void CreatePrefab(){
        if (true)
        {
            
        }
    }

    public override string ToString()
    {
        return $"Instance: {{ {this.Name}; {this.Type} }}";
    }

    private class ThingInstance{
        public string Thing { get; set; }
        public int Amount { get; set; }
        public int Percentage { get; set; }
    }
}
