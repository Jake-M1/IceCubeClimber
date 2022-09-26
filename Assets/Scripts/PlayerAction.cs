using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    public PlayerController controller;

    public Text healthText;
    public Text gameText;
    public Text controlsText;
    public int maxHealth = 30;
    public int playerKb = 400;
    public BoxCollider2D hurtbox;

    private int currentHealth;
    private int currentKb = 0;
    private bool kbRight;


    void Start()
    {
        currentHealth = maxHealth;
        gameText.text = "";
        SetCountText();
    }


    void Update()
    {
        checkControlsDisplay();
        checkHeight();
    }


    void FixedUpdate()
    {
        if (currentKb == playerKb)
        {
            if (kbRight == true)
            {
                controller.PlayerKnockback(currentKb, true);
                currentKb -= 100;
            }
            else
            {
                controller.PlayerKnockback(-currentKb, true);
                currentKb -= 100;
            }
        }
        else if (currentKb > 1)
        {
            if (kbRight == true)
            {
                controller.PlayerKnockback(currentKb, false);
                currentKb -= 100;
            }
            else
            {
                controller.PlayerKnockback(-currentKb, false);
                currentKb -= 100;
            }
        }
        else
        {
            currentKb = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (currentHealth < 30)
            {
                if (hurtbox.enabled == true)
                {
                    currentHealth += 5;
                }
                else
                {
                    currentHealth += 10;
                }
                other.gameObject.SetActive(false);
            }
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.x > other.gameObject.transform.position.x)
            {
                kbRight = true;
            }
            else
            {
                kbRight = false;
            }
            currentKb = playerKb;

            currentHealth -= 10;
            SetCountText();
            hurtbox.enabled = false;
            Invoke("EnableHurtbox", 2.0f);

            if (currentHealth <= 0)
            {
                PlayerDie();
            }
        }
        else if (other.gameObject.CompareTag("Spike"))
        {
            currentHealth -= 10;
            SetCountText();
            hurtbox.enabled = false;
            controller.PlayerKnockbackUp(playerKb);
            Invoke("EnableHurtbox", 2.0f);

            if (currentHealth <= 0)
            {
                PlayerDie();
            }
        }
        else if (other.gameObject.CompareTag("Win"))
        {
            PlayerWin();
        }
    }


    void EnableHurtbox()
    {
        hurtbox.enabled = true;
    }

    
    void SetCountText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
    

    private void PlayerDie()
    {
        gameText.text = "You Died :(";
        SceneManager.LoadScene("GameOverScreen");
    }

    private void PlayerWin()
    {
        gameText.text = "Congrats, you win! :)";
        SceneManager.LoadScene("Complete Screen");
    }


    private void checkControlsDisplay()
    {
        if (transform.position.x < 55 && transform.position.y < 15)
        {
            controlsText.text = "WASD -- move\nSpace -- jump\nLeft Click -- melee attack\nRight Click -- ranged attack\nQ -- switch background/foreground";
        }
        else
        {
            controlsText.text = "";
        }
    }


    private void checkHeight()
    {
        if (transform.position.y < -15)
        {
            PlayerDie();
        }
    }
}
