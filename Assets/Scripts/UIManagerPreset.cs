using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIManagerPreset 
{
    ManagerStatus status { get; set; }
    void StartUP();
}
public enum ManagerStatus
{
    Started,
    Closed
}
