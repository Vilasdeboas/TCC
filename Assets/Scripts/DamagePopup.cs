using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    private static bool hit;
    //CodeMonkey stolen code starts here
    public static Color GetColorFromString(string color) {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if(color.Length >= 8) {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    public static float Hex_to_Dec01(string hex) {
        return Hex_to_Dec(hex) / 255f;
    }

    public static int Hex_to_Dec(string hex) {
        return Convert.ToInt32(hex, 16);
    }
    //CodeMonkey code ends here


    public static DamagePopup Create(Vector3 position, string text, bool isHit) {
        hit = isHit;
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(text, isHit);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float disappearTime;
    private Color textColor;
    private float defaultFontSize = 8f;
    private float criticalHitFontSize = 10f;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string text, bool isHit) {
        textMesh.SetText(text);

        if(!isHit) {
            textMesh.fontSize = defaultFontSize;
            textColor = GetColorFromString("FF0000");
        } else {
            textMesh.fontSize = criticalHitFontSize;
            textColor = GetColorFromString("00FF00");
        }

        textMesh.color = textColor;
        disappearTime = 0.5f;
    }

    private void Update() {
        float moveYSpeed = hit ? 0.5f : -0.5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if(disappearTime < 0) {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) {
                Destroy(gameObject);
            }
        }
    }
}
