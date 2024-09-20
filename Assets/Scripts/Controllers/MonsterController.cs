using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;
    [SerializeField]
    float _scanRange = 10;
    [SerializeField]
    float _attackRange = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {
        worldobjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }
    }
    protected override void UpdateIdle()
    {
        Debug.Log("Monster UpdateIdle");

        GameObject player = Managers.Game.GetPlayer();

        if (player == null)
            return;
        float distance = (player.transform.position - transform.position).magnitude;
        if(distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }
    protected override void UpdateMoving()
    {
        Debug.Log("Monster UpdateMoving");

        if(_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }
        Vector3 dir = _destPos - transform.position;
        if(dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }
    protected override void UpdateSkill()
    {
        Debug.Log("Monster UpdateSkill");
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }
    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");
        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            //int damage = Mathf.Max(0, _stat.Attack - targetStat.Defence);
            //Debug.Log(damage);
            //targetStat.Hp -= damage;

            /*if(targetStat.Hp <= 0)
            {
                Managers.Game.Despawn(targetStat.gameObject);
            }*/
            targetStat.OnAttacked(_stat);
            if(targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if(distance <= _attackRange)
                {
                    State = Define.State.Skill;
                }
                else
                {
                    State = Define.State.Moving;
                }
            }
        }
        else
        {
            //Managers.Game.Despawn(targetStat.gameObject);
            State = Define.State.Idle;
        }
    }
}
