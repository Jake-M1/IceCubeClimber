using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    SpriteRenderer m_SpriteRenderer;
    BoxCollider2D m_BoxCollider;
    //public gameObject obj;
    private bool flipped;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        flipped = false;
        m_SpriteRenderer.sprite = sprite1;
        m_SpriteRenderer.sortingLayerName = "Foreground";
    }

    void Update()
    {
        if (Input.GetButtonDown("Flip Background"))
        {
            FlipBackground();
        }
    }

    private void FlipBackground()
    {
        if (flipped == false)
        {
            m_SpriteRenderer.sprite = sprite2;
            m_SpriteRenderer.sortingLayerName = "Background";
            m_BoxCollider.enabled = false;

            int bgTerrain = LayerMask.NameToLayer("BackgroundTerrain");
            this.gameObject.layer = bgTerrain;

            flipped = true;
        }
        else
        {
            m_SpriteRenderer.sprite = sprite1;
            m_SpriteRenderer.sortingLayerName = "Foreground";
            m_BoxCollider.enabled = true;

            int terrain = LayerMask.NameToLayer("Terrain");
            this.gameObject.layer = terrain;

            flipped = false;
        }
    }
}
