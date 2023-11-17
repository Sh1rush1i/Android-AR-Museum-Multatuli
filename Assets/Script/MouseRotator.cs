using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    public enum RotationType
    {
        Horizontal,
        Vertical,
        Both
    }

    public RotationType rotationType = RotationType.Both;
    public float rotationSpeed = 5.0f;

    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            float rotationX = 0.0f;
            float rotationY = 0.0f;

            switch (rotationType)
            {
                case RotationType.Horizontal:
                    rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime;
                    break;
                case RotationType.Vertical:
                    rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime;
                    break;
                case RotationType.Both:
                    rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime;
                    rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime;
                    break;
            }

            transform.Rotate(rotationX, rotationY, 0.0f, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }
}
