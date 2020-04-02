using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : ButtonCommand
{
    public void Execute()
    {
        Listener.Execute();
    }
}
