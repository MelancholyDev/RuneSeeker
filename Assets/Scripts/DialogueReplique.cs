using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System;

[Serializable]
public class DialogueReplique
{
    public DialogueReplique(int SerialNumber, string Sentence, int NextReplique, Who currentTalker, DialogueEvent CurEvent, int ImageName)
    {
        this.SerialNumber = SerialNumber;
        this.Sentence = Sentence;
        this.NextReplique = NextReplique;
        this.currentTalker = currentTalker;
        this.CurEvent = CurEvent;
        this.ImageName = ImageName;
    }
    public int SerialNumber;
    public string Sentence;
    public int NextReplique;
    public Who currentTalker;
    public int ImageName;
    public DialogueEvent CurEvent;

}
public enum Who
{
    Me = 1,
    Opponent = 2
}
[Serializable]
public class DialogueEvent
{
    public DialogueEvent(int Eventnum, int numofthings)
    {
        EventNum = Eventnum;
        NumOfThings = numofthings;
    }
    public int EventNum;
    public int NumOfThings;
}