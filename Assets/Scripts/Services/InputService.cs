using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

public class InputService : IInputService
{
    private readonly Camera _camera;
    private readonly Subject<List<Vector3>> _onPathUpdated = new();
    public IObservable<List<Vector3>> OnPathUpdated => _onPathUpdated;
    
    private readonly CompositeDisposable _disposables = new();
    private readonly List<Vector3> _path = new();

    private Vector3 _lastDirection = Vector3.zero;
    
    public InputService(Camera camera)
    {
        _camera = camera;
        SubscribeInput();
    }
    
    private void SubscribeInput()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => MouseDown())
            .AddTo(_disposables);
        
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => MouseMove())
            .AddTo(_disposables);
        
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => MouseUp())
            .AddTo(_disposables);
    }
    
    private void MouseDown()
    {
        _path.Clear();
        _lastDirection = Vector3.zero;
        AddPointToPath();
    }
    
    private void MouseMove()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        if (_path.Count == 0 || ShouldAddPoint(mousePosition))
        {
            _path.Add(mousePosition);
        }
    }
    
    private void MouseUp()
    {
        if (_path.Count > 0)
        {
            _onPathUpdated.OnNext(new List<Vector3>(_path));
        }
    }
    
    private void AddPointToPath()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        _path.Add(mousePosition);
    }
    
    private bool ShouldAddPoint(Vector3 newPoint)
    {
        Vector3 lastPoint = _path.Last();
        Vector3 direction = (newPoint - lastPoint).normalized;
        if (_lastDirection == Vector3.zero || Vector3.Dot(_lastDirection, direction) < 0.99f)
        {
            _lastDirection = direction;
            return true;
        }
        return false;
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
        _onPathUpdated.Dispose();
    }
}
