using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDebug : ButtonCommand
{
    public void Execute()
    {
        Listener.Execute();
    }
}
