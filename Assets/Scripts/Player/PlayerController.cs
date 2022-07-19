 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Transform movePoint;
    public LayerMask whatStopMovemet;
    public Joystick joystick;
    // Start is called before the first frame update
 [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererDeath;
   public static PlayerController instance;
    public int heart;
    private bool isDeath = false;
    private void Awake()
    {
        instance = this;
        heart = (int)Player.HEART;
    }
    void Start()
    {
        movePoint.parent = null;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f && !isDeath)
        {
            if (Mathf.Abs(joystick.Horizontal) >= .2f)
            {
                animator.SetFloat("moveHorizontal", joystick.Horizontal);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(joystick.Horizontal, 0f, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(joystick.Horizontal, 0f, 0f);
                }
            }
            else
            {
                animator.SetFloat("moveHorizontal", joystick.Horizontal);

            }
            if (Mathf.Abs(joystick.Vertical) >= .2f)
            {
                 animator.SetFloat("moveVertical", -joystick.Vertical);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, joystick.Vertical, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(0f, joystick.Vertical, 0f);
                }
            }
            else
            {
                animator.SetFloat("moveVertical", -joystick.Vertical);
            }
        }


    }
    //  private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Explosion"))
    //    {
    //        DeathSequence();
    //    }
    //}


    public void DeathSequence()
    {
        enabled = false;
        animator.SetBool("isDeath", true);
    }

    public IEnumerator CollisionEnemy(Collider2D player)
    {
        player.isTrigger = false;
        animator.SetBool("isDeath", true);
        isDeath = true;
        yield return new WaitForSeconds(1);
        isDeath = false;
        animator.SetBool("isDeath", false);
        yield return new WaitForSeconds(2);
        player.isTrigger = true;
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);

        FindObjectOfType<GameManager>().CheckGameState();


    }


}
