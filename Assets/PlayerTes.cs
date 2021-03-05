using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTes : MonoBehaviour
{

    private List<Sprite> sprites;
    private bool stopAnimation = false;

    void Awake()
    {
        sprites = new List<Sprite>();
        GetSpriteList();
    }

    public void GetSpriteList() {
        for(int i = 0; i < 6; i++) {
            sprites.Add(Resources.Load<Sprite>("anim_sprite/spriteidle_"+(i+1)));
        }
    }

    public void PlayAnimation() {
        stopAnimation = !stopAnimation;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void SetSpriteWrapper() {
        StartCoroutine(SetSprite());
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
}
