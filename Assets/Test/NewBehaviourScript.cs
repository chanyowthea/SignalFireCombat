using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

// BUG
// 只按一下J会无法播完攻击动画

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform _Player;
    [SerializeField] Animator _Animator;
    void Start()
    {

    }

    int _HitCount;
    private void Update()
    {
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

        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        if (h != 0)
        {
            Debug.Log("h=" + h);
            _Animator.SetBool("Run", true);
            _Player.transform.position += new Vector3(h, 0, 0) * 0.1f;
            Debug.Log(new Vector3(h, 0, 0) * 10); 
        }
        else
        {
            _Animator.SetBool("Run", false);
        }
        Debug.Log(_Player.transform.position); 

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    _Animator.SetBool("Run", true);
        //}
        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    _Animator.SetBool("Run", false);
        //}
        //Debug.Log("normalized=" + _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
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
