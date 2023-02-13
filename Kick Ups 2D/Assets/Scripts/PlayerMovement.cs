using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float mouseMoveX;
    private new Rigidbody2D rigidbody;
    private float mouseMoveY;
    public float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ground = GameObject.Find("Ground");
        GameObject rightColumn = GameObject.Find("ColumnRight");
        rigidbody = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();
        Collider2D groundCollider = ground.GetComponent<Collider2D>();
        Collider2D rightColumnCollider = rightColumn.GetComponent<Collider2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //mouseSensitivity = mouseSensitivity * 20;

        Physics2D.IgnoreCollision(groundCollider, collider);
        Physics2D.IgnoreCollision(rightColumnCollider, collider);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("Menu");
        }
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 mousePositionCam = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        //transform.position = mousePositionCam;

        mouseMoveX = Input.GetAxis("Mouse X");
        mouseMoveY = Input.GetAxis("Mouse Y");
        rigidbody.velocity = new Vector2(mouseMoveX * mouseSensitivity, mouseMoveY * mouseSensitivity);
    }

}
