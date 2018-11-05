using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour
{
    float _HurtGapTime = 0.2f;
    float _CurHurtTime;
    float _StayTime;
    float _DeferTime;

    public void SetData(Vector3 pos, Vector3 scale, float stayTime, float deferTime = 0)
    {
        transform.localPosition = pos;
        transform.localScale = scale;
        transform.localEulerAngles = Vector3.zero;
        _DeferTime = deferTime;
        _StayTime = stayTime; 
        StartCoroutine(DestroyRoutine());
    }

    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_DeferTime); 
        yield return new WaitForSeconds(_StayTime);
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player1")
        {
            if (Time.time - _CurHurtTime > _HurtGapTime)
            {
                _CurHurtTime = Time.time; 
                var player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.HP -= 1;
                }
            }
        }
    }
}
