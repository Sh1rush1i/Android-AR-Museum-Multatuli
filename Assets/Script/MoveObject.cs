using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Mendapatkan input dari tombol WASD atau panah
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Menghitung pergerakan berdasarkan input
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 moveVector = moveDirection.normalized * moveSpeed * Time.deltaTime;

        // Menggerakkan objek
        transform.Translate(moveVector);
    }
}
