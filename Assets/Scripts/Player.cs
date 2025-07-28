using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rb;

    private void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float xAxis = xMovement * moveSpeed * Time.deltaTime;
        rb.linearVelocity = new Vector2 (xAxis, 0);
        //transform.Translate(new Vector2(xAxis, 0));
    }


}
