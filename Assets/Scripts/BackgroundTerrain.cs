using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundTerrain : MonoBehaviour
{
    public TilemapRenderer tilemap1ARender;
    public TilemapRenderer tilemap2ARender;
    public TilemapRenderer tilemap1BRender;
    public TilemapRenderer tilemap2BRender;

    public TilemapCollider2D tilemap1ACollider;
    public TilemapCollider2D tilemap2ACollider;

    public TilemapRenderer tilemapSpike1ARender;
    public TilemapRenderer tilemapSpike1BRender;
    public TilemapCollider2D tilemapSpike1ACollider;

    public TilemapRenderer tilemapSpike2ARender;
    public TilemapRenderer tilemapSpike2BRender;
    public TilemapCollider2D tilemapSpike2ACollider;

    private bool flipped;


    void Start()
    {
        tilemap1ARender.enabled = true;
        tilemap2ARender.enabled = false;
        tilemap1BRender.enabled = false;
        tilemap2BRender.enabled = true;

        tilemap1ACollider.enabled = true;
        tilemap2ACollider.enabled = false;

        tilemapSpike1ARender.enabled = true;
        tilemapSpike1BRender.enabled = false;
        tilemapSpike1ACollider.enabled = true;

        tilemapSpike2ARender.enabled = false;
        tilemapSpike2BRender.enabled = true;
        tilemapSpike2ACollider.enabled = false;

        flipped = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Flip Background"))
        {
            FlipBackgroundTerrain();
        }
    }


    private void FlipBackgroundTerrain()
    {
        if (flipped == false)
        {
            tilemap1ARender.enabled = false;
            tilemap2ARender.enabled = true;
            tilemap1BRender.enabled = true;
            tilemap2BRender.enabled = false;

            tilemap1ACollider.enabled = false;
            tilemap2ACollider.enabled = true;

            tilemapSpike1ARender.enabled = false;
            tilemapSpike1BRender.enabled = true;
            tilemapSpike1ACollider.enabled = false;

            tilemapSpike2ARender.enabled = true;
            tilemapSpike2BRender.enabled = false;
            tilemapSpike2ACollider.enabled = true;

            flipped = true;
        }
        else
        {
            tilemap1ARender.enabled = true;
            tilemap2ARender.enabled = false;
            tilemap1BRender.enabled = false;
            tilemap2BRender.enabled = true;

            tilemap1ACollider.enabled = true;
            tilemap2ACollider.enabled = false;

            tilemapSpike1ARender.enabled = true;
            tilemapSpike1BRender.enabled = false;
            tilemapSpike1ACollider.enabled = true;

            tilemapSpike2ARender.enabled = false;
            tilemapSpike2BRender.enabled = true;
            tilemapSpike2ACollider.enabled = false;

            flipped = false;
        }
    }
}
