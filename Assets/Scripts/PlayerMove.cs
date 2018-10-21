using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 2;
    CharacterController _Controller; 
    [SerializeField] int _PlayerIndex; 
    [SerializeField] Text _PlayerName;

    private void Start()
    {
        _Controller = GetComponent<CharacterController>(); 
        var temp = transform.position;
        Debug.Log("temp=" + temp); 
        temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
        transform.position = temp; 
        _PlayerName.text = "Player " + _PlayerIndex;
        Debug.Log("transform.position=" + transform.position);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        float v = Input.GetAxis("Vertical" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        if ((v != 0 || h != 0))
        {
            //if (h != 0)
            //{
            //    var pos = new Vector3(h, 0, 0) * _MoveSpeed;
            //    Vector3 dir = Vector3.zero;
            //    dir.x = h > 0 ? 1 : -1;
            //    Ray ray = new Ray(transform.position, dir);
            //    RaycastHit hit;
            //    if (!Physics.Raycast(ray, out hit, _Controller.size.z / 2f + MapManager.Instance._MinGap, LayerMask.GetMask("Wall")))
            //    {
            //        _Controller.Move(pos);
            //        var temp = transform.position;
            //        temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
            //        transform.position = temp;
            //    }
            //}
            //if (v != 0)
            //{
            //    var pos = new Vector3(0, v, v * MapManager.Instance._DepthFactor) * _MoveSpeed;
            //    Vector3 dir = Vector3.zero;
            //    dir.z = v > 0 ? 1 : -1;
            //    Ray ray = new Ray(transform.position, dir);
            //    RaycastHit hit;
            //    if (!Physics.Raycast(ray, out hit, _Collider.size.z / 2f + MapManager.Instance._MinGap, LayerMask.GetMask("Wall")))
            //    {
            //        var temp = transform.position + pos;
            //        temp.y = MapManager.Instance.GetPosY(temp, _Collider.size.y);
            //        _Controller.MovePosition(temp);
            //    }
            //}

            var pos = new Vector3(h, v, v * MapManager.Instance._DepthFactor) * _MoveSpeed;
            _Controller.Move(pos); 
            var temp = transform.position;
            temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
            transform.position = temp;
        }
    }
}
