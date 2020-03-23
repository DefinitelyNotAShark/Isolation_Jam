using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivateable
{
    string NameText { get; }
    void DoActivate();
}