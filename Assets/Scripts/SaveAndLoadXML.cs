using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public static class SaveAndLoadXML
{
    public static T LoadFromXML<T>(string path) where T : new()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        string file = Path.Combine(Application.dataPath, path);
        if(File.Exists(file))
        {
            Stream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
            return (T)xmlSerializer.Deserialize(fileStream);
        }

        return new T();
    }

    public static void SaveToXML<T>(string path, T o) where T : new()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        string file = Path.Combine(Application.dataPath, path);
        if(File.Exists(file))
        {
            Stream fileStream = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            xmlSerializer.Serialize(fileStream, o);
        }
    }
}
