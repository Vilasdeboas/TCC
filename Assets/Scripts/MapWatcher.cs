using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWatcher : MonoBehaviour
{
    private static MapWatcher Instance;
    public static int CurrentMapIndex { 
        get { return current_map_index; }
        set { current_map_index = value; }
    }
    private static int current_map_index = 0;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
