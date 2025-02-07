using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService : IDisposable
{
    IObservable<List<Vector3>> OnPathUpdated { get; }
}