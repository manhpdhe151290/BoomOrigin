using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopMovemet;
    public Joystick joystick;
    // Start is called before the first frame update

   

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(joystick.Horizontal) >= .2f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(joystick.Horizontal, 0f, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(joystick.Horizontal, 0f, 0f);
                }
            }
            if (Mathf.Abs(joystick.Vertical) >= .2f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, joystick.Vertical, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(0f, joystick.Vertical, 0f);
                }
            }
        }


    }
}
