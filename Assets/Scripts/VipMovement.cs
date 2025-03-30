using UnityEngine;

public class VIPController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}