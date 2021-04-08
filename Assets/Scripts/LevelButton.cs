using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    void Start()
    {
        Transform text = gameObject.transform.Find("Text");
        text.GetComponent<Text>().text = "Level " + (level + 1);
        text.position = text.position + new Vector3(0, 1);
    }

}
