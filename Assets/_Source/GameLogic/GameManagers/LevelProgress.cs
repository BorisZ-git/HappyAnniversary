using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public static class LevelProgress
{
    static string path = "Progress.xml";
    static SaveProgress _progress;
    static SaveLevel _saveLvl;
    public static void SaveProgress()
    {
        _progress = WriteProgress();
        string[] temp = SceneManager.GetActiveScene().name.Split("_");
        _saveLvl = new SaveLevel($"{temp[0]} {temp[1]}", SceneManager.GetActiveScene().name);
        if (_progress.IsLevelSaved(_saveLvl))
        {
            return;
        }
        else
        {
            _progress.saveProgress.Add(_saveLvl);
            XmlSerializer ser = new XmlSerializer(typeof(SaveProgress));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, _progress);
            }
        }
    }
    public static SaveProgress WriteProgress()
    {
        SaveProgress temp = new SaveProgress();
        if (File.Exists(path))
        {
            XmlSerializer ser = new XmlSerializer(typeof(SaveProgress));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                temp = (SaveProgress)ser.Deserialize(fs);
            }
        }
        return temp;
    }
}
