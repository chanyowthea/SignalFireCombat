using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int _PlayerIndex;
    PlayerMove _PlayerMove;
    [SerializeField] Text _HPText;

    float _HP = 40;

    public float HP
    {
        set
        {
            if (value <= 0)
            {
                OnDie();
                return;
            }
            _HP = value;
            UpdateView();
        }
        get
        {
            return _HP;
        }
    }

    void Awake()
    {
        _PlayerMove = GetComponent<PlayerMove>();
        _PlayerMove.SetData(_PlayerIndex);
    }

    private void Start()
    {
        UpdateView();
    }

    void UpdateView()
    {
        _HPText.text = Mathf.CeilToInt(_HP).ToString();
    }

    void OnDie()
    {
        GameObject.Destroy(this.gameObject); 
    }

    void Update()
    {

    }
}
