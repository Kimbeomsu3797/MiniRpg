using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    //프로젝트 창에서 보여주기 위해
    [SerializeField]
    protected int _lavel;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defence;
    [SerializeField]
    protected float _moveSpeed;

    //외부에서 접근할 수 있게 프로퍼티로 생성 해줌
    //즉, 플레이어 적 둘다 사용하는 것들
    public int Level { get { return _lavel; } set { _lavel = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defence { get { return _defence; } set { _defence = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        _lavel = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defence = 5;
        _moveSpeed = 5f;
    }
    public virtual void OnAttacked (Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }
    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat; //attacker 를 PlayerStat 캐스팅
        if(playerStat != null)
        {
            playerStat.Exp += 15;
        }
        Managers.Game.Despawn(gameObject);
    }
}
