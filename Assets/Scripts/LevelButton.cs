using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    public string prefix;
    private Text btn_text;
    void Awake()
    {
        Transform text = gameObject.transform.Find("Text");
        btn_text = text.GetComponent<Text>();
        SetText();
        //text.position = text.position + new Vector3(0, 1);
    }

    public void SetText() {
        btn_text.text = prefix+" " + (level + 1);
    }

}
