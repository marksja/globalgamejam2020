using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationWriter : MonoBehaviour
{
    public ConversationData data;
    public string filePath;

    [ContextMenu("SaveFile")]
    public void SaveData()
    {
        SaveAndLoadXML.SaveToXML<ConversationData>(filePath, data);
    }
}
