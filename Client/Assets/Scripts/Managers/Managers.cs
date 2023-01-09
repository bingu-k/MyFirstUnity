using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    MapManager _map = new MapManager();
    ObjectManager _object = new ObjectManager();

    public static MapManager Map { get { return Instance._map; } }
    public static ObjectManager Object { get { return Instance._object; } }
    #endregion

    #region Core
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    PoolManager _pool = new PoolManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

    static public void Clear()
    {

    }
}
