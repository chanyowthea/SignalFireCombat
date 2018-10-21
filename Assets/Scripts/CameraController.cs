using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] PlayerMove _CurrentPlayer;
    Camera _PlayerCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _PlayerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        SetCameraPos(_CurrentPlayer.transform.position);
    }

    void SetCameraPos(Vector3 playerPos)
    {
        float screenRatio = Screen.width / (float)Screen.height;
        playerPos.y = Mathf.Clamp(playerPos.y, MapManager.Instance._MapRect._Bottom + _PlayerCamera.orthographicSize,
            MapManager.Instance._MapRect._Top - _PlayerCamera.orthographicSize);
        playerPos.x = Mathf.Clamp(playerPos.x, 
            MapManager.Instance._MapRect._Left + _PlayerCamera.orthographicSize * screenRatio,
            MapManager.Instance._MapRect._Right - _PlayerCamera.orthographicSize * screenRatio);
        playerPos.z = transform.position.z;
        transform.transform.position = playerPos;
    }
}
