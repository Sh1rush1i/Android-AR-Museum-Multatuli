using UnityEngine;

public class Toggle : MonoBehaviour
{
    private bool isObjectEnabled = true; // Status awal objek

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 represents the right mouse button
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log("Right mouse button clicked at (" + mousePosition.x + ", " + mousePosition.y + ")");

            // Ubah status objek secara bergantian
            isObjectEnabled = !isObjectEnabled;

            // Aktifkan atau nonaktifkan objek berdasarkan status
            gameObject.SetActive(isObjectEnabled);
        }
    }
}
