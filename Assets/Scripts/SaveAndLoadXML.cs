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

        string file = Path.Combine(Application.streamingAssetsPath, path);
        if(File.Exists(file))
        {
            Stream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
            T obj = (T)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();

            return obj;
        }

        return new T();
    }

    public static void SaveToXML<T>(string path, T o) where T : new()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        string file = Path.Combine(Application.dataPath, path);

        Stream fileStream = File.Open(file, FileMode.Create, FileAccess.ReadWrite);
        xmlSerializer.Serialize(fileStream, o);

        fileStream.Close();
    }
}
