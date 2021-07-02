using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageGenerator {
    private static string[] hitMessages = new[] {"Boa!", "Legal!", "Mandou bem!", ";)", ":)", ":D", ";D", "xD"};
    private static string[] missMessages = new[] { "Quase!", "Opa!", "Tente novamente!", "Passou raspando!", ":S", ":|", ":T"};
    private static string[] enemyHitMessages = new[] { "Eita!", "Essa doeu!", "Oof", "Dx", "Ai!", ":C", "Ui ui!" };

    static public string getHitMessage() {
        int pos = Random.Range(0, hitMessages.Length);
        return hitMessages[pos];
    }

    static public string getMissMessage() {
        int pos = Random.Range(0, missMessages.Length);
        return missMessages[pos];
    }

    static public string getEnemyHitMessage() {
        int pos = Random.Range(0, missMessages.Length);
        return enemyHitMessages[pos];
    }
}
