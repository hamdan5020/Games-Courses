using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Config
    [SerializeField] AudioClip breakSoundFX;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // State Vars

    [SerializeField] int hits; // SerializedField for debugging purposes Remove later

    // Start is called before the first frame update
    Level level;
    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (tag == "Breakable")
        {
            TriggerSparklesVFX();

            hits++;
             int maxHits = hitSprites.Length + 1;
            if (hits >= maxHits)
            {
                FindObjectOfType<GameSession>().AddToScore();
                DestroyBlock();
                

            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = hits - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array on " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(breakSoundFX, Camera.main.transform.position, 0.1f);
        level.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

        Destroy(sparkles, 1f);
    }
}
