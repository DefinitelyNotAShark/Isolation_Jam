using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonCommand
{
    public string Name;
    public IButtonListener Listener;
}
