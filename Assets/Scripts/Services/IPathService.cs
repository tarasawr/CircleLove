using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public interface IPathService : IDisposable
    {
        IObservable<List<Vector3>> OnPathUpdated { get; }
    }
}