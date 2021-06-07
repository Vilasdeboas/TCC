using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{

    private static float time;
    private static float defaultTime = 20;
    private static bool isRunning = false;
    private static bool hasEnded = false;
    public Image fillImage;
    public Text fillText;

    public static void Run() {
        time = defaultTime;
        isRunning = true;
        hasEnded = false;
    }

    public static void Stop() {
        isRunning = false;
    }

    public static bool Ended() {
        return hasEnded;
    }

    private void UpdateFillImage() {
        fillImage.fillAmount = time / defaultTime;
        if (time <= 0) {
            fillText.text = "0";
        } else {
            fillText.text = System.Math.Round(time, 0).ToString();
        }
    }

    public static void ResetEndedStatus() {
        hasEnded = false;
    }

    void Update() {
        if (isRunning) {
            if (time > 0) {
                time -= Time.deltaTime;
            } else {
                isRunning = false;
                hasEnded = true;
            }
            UpdateFillImage();
        }
    }
}
