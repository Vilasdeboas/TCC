using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerTes : MonoBehaviour
{

    private List<Sprite> sprites;
    private List<Sprite> spritesattack;
    private bool stopAnimation = false;

    void Awake()
    {
        sprites = new List<Sprite>();
        spritesattack = new List<Sprite>();
        GetSpriteList();
    }

    public void GetSpriteList() {
        string[] idleSprites = Directory.GetFiles("Assets/Resources/anim_sprite/idle/", "*.psd");
        string[] attackSprites = Directory.GetFiles("Assets/Resources/anim_sprite/attack/", "*.psd");
        for(int i = 0; i < idleSprites.Length; i++) {
            sprites.Add(Resources.Load<Sprite>("anim_sprite/idle/spriteidle_"+(i+1)));
        }

        for(int i = 0; i < attackSprites.Length; i++) {
            spritesattack.Add(Resources.Load<Sprite>("anim_sprite/attack/spriteattack_" + (i + 1)));
        }
    }

    public void PlayAnimation() {
        stopAnimation = !stopAnimation;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void SetSpriteWrapper() {
        StartCoroutine(SetSprite());
    }

    public void SetAttackSpriteWrapper() {
        StartCoroutine(SetAttackSprite());
    }
    public IEnumerator SetSprite() {
        if(!stopAnimation) { 
            for(int i = 0; i < sprites.Count; i++) {
                if(stopAnimation) break;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
                yield return new WaitForSeconds(0.05f);
            }
            SetSpriteWrapper();
        }
    }
    public IEnumerator SetAttackSprite() {
        if(!stopAnimation) {
            for(int i = 0; i < spritesattack.Count; i++) {
                if(stopAnimation) break;
                gameObject.GetComponent<SpriteRenderer>().sprite = spritesattack[i];
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}
