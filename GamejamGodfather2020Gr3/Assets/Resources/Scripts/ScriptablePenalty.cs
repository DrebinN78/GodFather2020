using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public enum effect
{
    Jump,
    Speed,
    Invisibility,
    Weight,
    Autojump,
    Gravity
}

[CreateAssetMenu(fileName = "New Penalty", menuName = "Penalty")]
public class ScriptablePenalty : ScriptableObject
{
    public effect effectType = effect.Jump;
    [Range(0f, 2f)] public float valueModifier = 1; // the multiplier of the current value
    public float effectDuration = 3f;
    private GameObject player;

            //case effect.Weight:
                //get player rigidbody
                //create temp var that contain player initial rigidbody value
                //verify collision between player
                //get the position of the player colliding
                //add force in the opposite direction (same force as a dash or not ?)
                //set player's rigidbody weight to something else
                //break;
    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public IEnumerator Jump()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        float initJumpForce = pc.jumpForce;
        pc.jumpForce *= valueModifier;
        yield return new WaitForSeconds(effectDuration);
        pc.jumpForce = initJumpForce;
        GameManager.instance.ResetTimer();
    }

    public IEnumerator Speed()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        float initSpeed = pc.speed;
        pc.speed *= valueModifier;
        yield return new WaitForSeconds(effectDuration);
        pc.speed = initSpeed;
        GameManager.instance.ResetTimer();
    }

    public IEnumerator Invisibility()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        //Sprite initSprite = sr.sprite;
        yield return new WaitForSeconds(effectDuration);
        ///sr.sprite = initSprite;
        sr.enabled = true;
        GameManager.instance.ResetTimer();
    }

    public IEnumerator Weight()
    {
        yield return 0;
    }

    public IEnumerator AutoJump()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        float timer = 0;
        while (timer < effectDuration)
        {
            if (pc.isGrounded)
            {
                Rigidbody2D rb = pc.GetComponentInParent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, pc.jumpForce);
            }
            timer += Time.deltaTime;
        }
        GameManager.instance.ResetTimer();
        yield return 0;
    }

    public IEnumerator Gravity()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = valueModifier;
        yield return new WaitForSeconds(effectDuration);
        rb.gravityScale = 1;
    }
}
