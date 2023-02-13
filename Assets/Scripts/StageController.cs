using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("GameObject have exited the trigger area: " + collision.gameObject.name);
        if (collision.gameObject.name == "Ball" || collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.position = new Vector3(0,0,0);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0f,0f);
        }
    }
}
