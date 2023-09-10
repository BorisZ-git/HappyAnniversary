using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
namespace Platformer.MessageEditor
{
    public static class WriteLoadText 
    {
        public static void SerializeFile(MessageData messageData,string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(MessageData));
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, messageData);
            }
        }
        public static MessageData DeserializeFile(string path) 
        {
            XmlSerializer ser = new XmlSerializer(typeof(MessageData));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (MessageData)ser.Deserialize(fs);
            }           
        }

        public static List<MessageData> GetDataFromDirectory(string directoryPath)
        {
            List<MessageData> messageDatas = new List<MessageData>();
            if (Directory.Exists(directoryPath))
            {
                List<string> vs = Directory.GetFiles(directoryPath, "*.xml").ToList();
                foreach (var item in vs)
                {
                    try
                    {

                            messageDatas.Add(DeserializeFile(item));
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error with deserialize file: {0}" + e.ToString());
                    }
                }
            }
            return messageDatas;
        }
    }
}

