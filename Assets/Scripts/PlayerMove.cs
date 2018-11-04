using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 5;
    [SerializeField] float _WalkSpeed = 0.8f;
    [SerializeField] CharacterController _Controller;
    [SerializeField] Text _PlayerName;

    [SerializeField] Transform _Player;
    [SerializeField] Animator _Animator;
    [SerializeField] FireBox _FireBox; 
    
    int _PlayerIndex;
    bool _IsJumping;
    int _HitCount;
    int _MoveState;
    //float _PhysicalValue = 100; 
    float _LastWalkTime; 

    public void SetData(int playerIndex)
        {
        _PlayerIndex = playerIndex; 
        }

    private void Start()
    {
        var temp = transform.position;
        temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
        transform.position = temp;
        _PlayerName.text = "Player " + _PlayerIndex;
    }

    void Update()
    {
        if (_PlayerIndex != 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (_MoveState == 0)
            {
                if (Time.time - _LastWalkTime < 0.5f)
                {
                    _MoveState = 2;
                }
                else
                {
                    _MoveState = 1;
                }
            }
            if (_MoveState == 1)
            {
                _LastWalkTime = Time.time;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _MoveState = 0;
        }

        float h = Input.GetAxis("Horizontal" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        float v = Input.GetAxis("Vertical" + (_PlayerIndex != 0 ? _PlayerIndex.ToString() : ""));
        if ((v != 0 || h != 0))
        {
            // cannot move vertical while jumping
            if (_IsJumping)
            {
                v = 0;
            }

            float speed = _WalkSpeed;
            if (_MoveState == 2)
            {
                speed = _MoveSpeed;
            }
            var pos = new Vector3(h, v, v * MapManager.Instance._DepthFactor) * speed;
            _Controller.Move(pos);
            var temp = transform.position;
            temp.y = MapManager.Instance.GetPosY(temp, _Controller.height);
            transform.position = temp;
            _Animator.SetBool("Walk", false);
            _Animator.SetBool("Run", false);
            _Animator.SetBool(_MoveState == 2 ? "Run" : "Walk", true);
            _Player.transform.localEulerAngles = new Vector3(0, h < 0 ? 180 : 0, 0);
        }
        else
        {
            _Animator.SetBool("Walk", false);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_IsJumping)
            {
                _Animator.CrossFade("Jump", 0);
                StartCoroutine(JumpRoutine());
            }
        }
    }

    IEnumerator JumpRoutine()
    {
        _IsJumping = true;
        Vector3 originPos = _Controller.transform.position;
        var destPos = _Controller.transform.position + new Vector3(0, 50, 0);
        var p = Vector3.zero;
        // jump up
        float maxTime = 0.5f;
        float time = 0;
        while (time < maxTime)
        {
            yield return null;
            time += Time.deltaTime;
            var pos = Vector3.Lerp(originPos, destPos, time / maxTime);
            p = _Controller.transform.position;
            p.y = pos.y;
            _Controller.transform.position = p;
        }
        p = _Controller.transform.position;
        p.y = destPos.y;
        _Controller.transform.position = p;

        // fall down
        time = 0;
        while (time < maxTime)
        {
            yield return null;
            time += Time.deltaTime;
            var pos = Vector3.Lerp(destPos, originPos, time / maxTime);
            p = _Controller.transform.position;
            p.y = pos.y;
            _Controller.transform.position = p;
        }
        p = _Controller.transform.position;
        p.y = originPos.y;
        _Controller.transform.position = p;
        _IsJumping = false;
    }

    void Attack()
    {
        var state = _Animator.GetCurrentAnimatorStateInfo(0);
        if (_HitCount == 0 && state.normalizedTime > 0f)
        {
            _Animator.SetInteger("AttackState", 1);
            _HitCount = 1;
            var box = GameObject.Instantiate(_FireBox); 
            box.gameObject.SetActive(true); 
            box.SetData(this.transform.position + new Vector3(47, 0, 0), new Vector3(74, 53, 30)); 
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
