using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloatRect
{
    public FloatRect(float left, float right, float top, float bottom)
    {
        _Left = left;
        _Right = right;
        _Top = top;
        _Bottom = bottom;
    }

    public float _Left;
    public float _Right;
    public float _Top;
    public float _Bottom;
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public readonly float _MinGap = 1f;
    public readonly float _DepthFactor = 2;
    public readonly float _PlayerDefaultBodyDepth = 20;
    bool _HasInited;

    [SerializeField] float _RoadHeight = 92;
    [SerializeField] GameObject _FrontWall;
    [SerializeField] GameObject _BackWall;
    [SerializeField] GameObject _LeftWall;
    [SerializeField] GameObject _RightWall;
    [SerializeField] Camera _Camera;

    [SerializeField] public float _BottomPosY;
    [SerializeField] public float _TopPosY;
    [SerializeField] float _RoadOffset = 10f;
    [SerializeField] public FloatRect _MapRect;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        if (_HasInited)
        {
            return;
        }
        _HasInited = true;

        // set wall
        var pos = _FrontWall.transform.position;
        pos.z = -_RoadHeight / 2f * _DepthFactor - _PlayerDefaultBodyDepth / 2f;
        _FrontWall.transform.position = pos;
        pos = _BackWall.transform.position;
        pos.z = _RoadHeight / 2f * _DepthFactor + _PlayerDefaultBodyDepth / 2f;
        _BackWall.transform.position = pos;
        _LeftWall.transform.localScale = new Vector3(1, _BackWall.transform.localScale.y, 
            _BackWall.transform.position.z - _FrontWall.transform.position.z);
        _RightWall.transform.localScale = _LeftWall.transform.localScale;
        _LeftWall.transform.position = new Vector3(-_BackWall.transform.localScale.x / 2f, 0, 0);
        _RightWall.transform.position = new Vector3(_BackWall.transform.localScale.x / 2f, 0, 0);

        _BottomPosY = -_Camera.orthographicSize + _RoadOffset;
        _TopPosY = _BottomPosY + _RoadHeight;
        _MapRect._Left = -_BackWall.transform.localScale.x / 2f;
        _MapRect._Right = _BackWall.transform.localScale.x / 2f;
        _MapRect._Bottom = -_BackWall.transform.localScale.y / 2f; 
        _MapRect._Top = _BackWall.transform.localScale.y / 2f; 
    }

    public void Clear()
    {
        _HasInited = false;
    }

    public float GetPosZ(Vector3 pos, float playerHeight)
    {
        pos.y -= playerHeight / 2f;
        float totalz = _RoadHeight * _DepthFactor;
        float ratio = (pos.y - _BottomPosY - _RoadHeight / 2) / _RoadHeight;
        return totalz * ratio;
    }

    public float GetPosY(Vector3 pos, float playerHeight)
    {
        float ratio = pos.z / (_RoadHeight * _DepthFactor);
        return ratio * _RoadHeight + _BottomPosY + _RoadHeight / 2f + playerHeight / 2f;
    }

    // need to delete
    //private void Start()
    //{
    //    Init();
    //}

    private void OnDestroy()
    {
        Clear();
    }
}
