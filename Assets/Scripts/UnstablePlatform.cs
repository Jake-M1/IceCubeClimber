using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnstablePlatform : MonoBehaviour
{
    public TilemapRenderer tilemapARender;
    public TilemapRenderer tilemapBRender;
    public TilemapRenderer tilemapCRender;

    public TilemapCollider2D tilemapACollider;
    public TilemapCollider2D tilemapBCollider;
    public TilemapCollider2D tilemapCCollider;

    public float top;

    private bool going;

    void Start()
    {
        going = false;
        SetStateA();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && going == false)
        {
            if (other.gameObject.transform.position.y >= top)
            {
                //Debug.Log("player entered unstable platform");
                going = true;
                StartCoroutine(StartSequence());
            }
        }
    }

    IEnumerator StartSequence()
    {
        SetStateA();
        yield return new WaitForSeconds(1);
        SetStateB();
        yield return new WaitForSeconds(1);
        SetStateC();
        yield return new WaitForSeconds(1);
        SetStateD();
        yield return new WaitForSeconds(3);
        SetStateA();
        going = false;
    }

    private void SetStateA()
    {
        tilemapARender.enabled = true;
        tilemapBRender.enabled = false;
        tilemapCRender.enabled = false;

        tilemapACollider.enabled = true;
        tilemapBCollider.enabled = false;
        tilemapCCollider.enabled = false;
    }

    private void SetStateB()
    {
        tilemapARender.enabled = false;
        tilemapBRender.enabled = true;
        tilemapCRender.enabled = false;

        tilemapACollider.enabled = false;
        tilemapBCollider.enabled = true;
        tilemapCCollider.enabled = false;
    }

    private void SetStateC()
    {
        tilemapARender.enabled = false;
        tilemapBRender.enabled = false;
        tilemapCRender.enabled = true;

        tilemapACollider.enabled = false;
        tilemapBCollider.enabled = false;
        tilemapCCollider.enabled = true;
    }

    private void SetStateD()
    {
        tilemapARender.enabled = false;
        tilemapBRender.enabled = false;
        tilemapCRender.enabled = false;

        tilemapACollider.enabled = false;
        tilemapBCollider.enabled = false;
        tilemapCCollider.enabled = false;
    }
}
