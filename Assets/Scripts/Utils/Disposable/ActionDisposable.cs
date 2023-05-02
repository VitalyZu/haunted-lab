using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDisposable : IDisposable
{
    private Action _onDispose;

    public ActionDisposable(Action action)
    {
        _onDispose = action;
    }

    public void Dispose()
    {
        _onDispose?.Invoke();
    }
}
