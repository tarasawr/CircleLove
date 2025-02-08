using System;
using UnityEngine;

namespace Services
{
    public interface IInputService : IDisposable
    {
        IObservable<Vector3> OnMouseDown { get; }
        IObservable<Vector3> OnMouseDrag { get; }
        IObservable<Vector3> OnMouseUp { get; }
    }
}