using System;
using UnityEngine;

public class SharedFloat
{
    public Action<float> OnValueChanged;

    private float value;
    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke(this.value);
        }
    }
}
