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
    
    [SerializeField] Transform _Player;
    [SerializeField] Animator _Animator;

    int _HitCount;

    private void Start()
    {
        _Controller = GetComponent<CharacterController>(); 
        var temp = transform.position;
        temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
        transform.position = temp; 
        _PlayerName.text = "Player " + _PlayerIndex;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        float v = Input.GetAxis("Vertical" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        if ((v != 0 || h != 0))
        {
            var pos = new Vector3(h, v, v * MapManager.Instance._DepthFactor) * _MoveSpeed;
            _Controller.Move(pos); 
            var temp = transform.position;
            temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
            transform.position = temp;
            _Animator.SetBool("Run", true);
            _Player.transform.localEulerAngles = new Vector3(0, h < 0 ? 180 : 0, 0);
        }
        else
        {
            _Animator.SetBool("Run", false);
        }

        var state = _Animator.GetCurrentAnimatorStateInfo(0);
        if (_HitCount != 0 && state.normalizedTime > 1)
        {
            _Animator.SetInteger("AttackState", 0);
            _HitCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _Animator.CrossFade("Jump", 0);
        }
    }

    void Attack()
    {
        var state = _Animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log("Attack" + state.normalizedTime);
        if (_HitCount == 0 && state.normalizedTime > 0.5f)
        {
            _Animator.SetInteger("AttackState", 1);
            _HitCount = 1;
        }
        else if (_HitCount == 1 && state.normalizedTime > 0.5f)
        {
            _Animator.SetInteger("AttackState", 2);
            _HitCount = 2;
        }
        else if (_HitCount == 2 && state.normalizedTime > 0.5f)
        {
            Debug.Log("Attack" + state.normalizedTime + ", " + _HitCount);
            _Animator.SetInteger("AttackState", 3);
            _HitCount = 3;
        }
    }
}
