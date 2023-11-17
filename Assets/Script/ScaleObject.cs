using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public float minScale = 0.1f;       // Skala minimum
    public float maxScale = 2.0f;       // Skala maksimum
    public float scrollSpeed = 0.1f;    // Kecepatan perubahan skala dengan scroll

    void Update()
    {
        // Mendapatkan input dari mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Menghitung perubahan skala berdasarkan input scroll wheel
        float newScale = transform.localScale.x + scrollInput * scrollSpeed;

        // Membatasi skala agar tidak lebih kecil dari minimum atau lebih besar dari maksimum
        newScale = Mathf.Clamp(newScale, minScale, maxScale);

        // Menetapkan skala baru ke objek
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
