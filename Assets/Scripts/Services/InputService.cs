using System;
using UniRx;
using UnityEngine;
using Zenject;


public class InputService : IInputService
{
    private readonly Camera _camera;
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private readonly Subject<Vector3> _onMouseDown = new Subject<Vector3>();
    public IObservable<Vector3> OnMouseDown => _onMouseDown;

    private readonly Subject<Vector3> _onMouseDrag = new Subject<Vector3>();
    public IObservable<Vector3> OnMouseDrag => _onMouseDrag;

    private readonly Subject<Vector3> _onMouseUp = new Subject<Vector3>();
    public IObservable<Vector3> OnMouseUp => _onMouseUp;

    [Inject]
    public InputService(Camera camera)
    {
        _camera = camera;
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0f;
                _onMouseDown.OnNext(pos);
            })
            .AddTo(_disposables);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0f;
                _onMouseDrag.OnNext(pos);
            })
            .AddTo(_disposables);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ =>
            {
                Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0f;
                _onMouseUp.OnNext(pos);
            })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _onMouseDown.Dispose();
        _onMouseDrag.Dispose();
        _onMouseUp.Dispose();
    }
}