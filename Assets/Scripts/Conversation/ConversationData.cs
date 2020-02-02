using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
[XmlRoot("ConversationData")]
public class ConversationData
{
    public List<ConversationLine> conversationLines;    
}

[System.Serializable]
public class ConversationLine
{
    [XmlAttribute("SpeakerID")]
    public string speakerID = "";
    [XmlAttribute("SpeakerAnimationID")]
    public string speakerAnimation = "";
    [XmlAttribute("AudioTriggerID")]
    public string audioTrigger = "";
    [XmlAttribute("DisplayTime")]
    public float timeToDisplay = -1;
    
    [XmlText]
    public string dialogue;
}
