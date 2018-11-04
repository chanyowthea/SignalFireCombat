using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour
{
    float _HurtGapTime = 0.2f;
    float _CurHurtTime;

    public void SetData(Vector3 pos, Vector3 scale)
    {
        transform.position = pos;
        transform.localScale = scale;
        StartCoroutine(DestroyRoutine());
    }

    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player1")
        {
            if (Time.time - _CurHurtTime > _HurtGapTime)
            {
                Debug.LogError("Player Hit"); 
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
