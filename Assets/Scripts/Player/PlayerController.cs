using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2f;
    public Transform movePoint;
    public LayerMask whatStopMovemet;
    InputManager inputManager;
    float horizontalMove = 0;
    float verticalMove = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Enable();
        inputManager.Land.HorizontalMove.performed += ctx =>
        {
            horizontalMove = ctx.ReadValue<float>();
        };
        inputManager.Land.VerticalMove.performed += ctx =>
        {
            verticalMove = ctx.ReadValue<float>();
        };
    }

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(horizontalMove) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalMove, 0f, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(horizontalMove, 0f, 0f);
                }
            }
            if (Mathf.Abs(verticalMove) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalMove, 0f), .2f, whatStopMovemet))
                {
                    movePoint.position += new Vector3(0f, verticalMove, 0f);
                }
            }
        }


    }
}
