using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class SaveLevel
{
    public string name;
    public string buildName;
    public int index;
    public SaveLevel() { }
    public SaveLevel(string name, string buildName) 
    {
        this.name = name;
        this.buildName = buildName;
        string[] temp = name.Split(" ");
        if(int.TryParse(temp[1],out int i))
        {
            index = i-1;
        }
        else
        {
            index = 0;
        }
    }
}
[Serializable]
public class SaveProgress
{
    public List<SaveLevel> saveProgress = new List<SaveLevel>();
    public SaveProgress() { }


    [XmlIgnore]
    public int GetCount { get => saveProgress.Count; }
    public string GetName(int index)
    {
        return saveProgress[index].name;
    }
    public SaveLevel GetContainLevel(string compareName)
    {
        foreach (var item in saveProgress)
        {
            if(item.name == compareName)
            {
                return item;
            }
        }
        return null;
    }
    public bool IsLevelSaved(SaveLevel saveLevel)
    {
        foreach (var item in saveProgress)
        {
            if(saveLevel.buildName == item.buildName)
            {
                return true;
            }
        }
        return false;
    }
}

