using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class OrbitCam : MonoBehaviour
{
    private Vector2 input;
    private Vector2 orbitAngles = new Vector2(45f, 0);
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float distance = 5f;


    public Transform focus;


    //--------------------------------------------\\


    void LateUpdate()
    {

        Quaternion lookRotation = transform.localRotation;

        if (ManualRotation())
        {
            orbitAngles.x = Mathf.Clamp(orbitAngles.x, -70, 70);
            lookRotation = Quaternion.Euler(orbitAngles);
        }

        lookRotation = Quaternion.Euler(orbitAngles);

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focus.position - lookDirection * distance;

        transform.SetPositionAndRotation(lookPosition, lookRotation);

    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //------------------------------------------------------------------------------------------\\


    private void OnLook(InputValue value)
    {
        input.x = value.Get<Vector2>().y;
        input.y = value.Get<Vector2>().x;
    }

    bool ManualRotation()
    {
        float deadzone = 0.001f;

        if (input.magnitude > deadzone)
        {
            orbitAngles += input * rotationSpeed * Time.unscaledDeltaTime;
            return true;
        }

        return false;
    }
}
