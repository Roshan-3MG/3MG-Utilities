﻿using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();
    protected virtual void Awake()
    {
        applicationIsQuitting = false;
    }
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                //Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    //"' already destroyed on application quit." +
                    //" Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return instance;
                    }

                    if (instance == null)
                    {
                        //Debug.LogWarning("[Singleton] Does not exist in the scene.");


                        // Create singleton
                        // GameObject singleton = new GameObject();
                        // _instance = singleton.AddComponent<T>();
                        // singleton.name = "(singleton) " + typeof(T).ToString();
                        //
                        // DontDestroyOnLoad(singleton);

                        // Debug.Log("[Singleton] An instance of " + typeof(T) + 
                        //  " is needed in the scene, so '" + singleton +
                        //  "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        //Debug.Log("[Singleton] Using instance already created: " +
                        //    instance.gameObject.name);
                    }
                }

                return instance;
            }
        }
    }

    private static bool applicationIsQuitting;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}