using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent; // int¸¦ Àü´Þ

    public GameObject GetPlayer() { return _player; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.worldobjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);
        switch (type)
        {
            case Define.WorldObject.Player:
                if (_player == go)
                    _player = null;
                break;
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if(OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                        
                    }
                }
                break;

        }

        Managers.Resources.Destroy(go);
    }
    public GameObject Spawn(Define.WorldObject type , string path, Transform parent = null)
    {
        GameObject go = Managers.Resources.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                break;
        }
        return null;
    }
}
