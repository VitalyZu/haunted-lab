using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableProperty<TType>
{
    private TType _value;

    public delegate void OnChangeProperty(TType oldValue, TType newValue);
    public event OnChangeProperty OnChange;

    public TType Value 
    {
        get { return _value; }
        set 
        {
            if (value.Equals(_value)) return;
            TType oldValue = _value;
            _value = value;
            OnChange?.Invoke(oldValue, _value);
        }
    }

    public IDisposable Subscribe(OnChangeProperty call)
    {
        OnChange += call;
        return new ActionDisposable(()=> OnChange -= call);
    }
}
