using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;

    [Header("Mouse")]
    public float mouseSensitivity = 5f;
    public float verticalClamp = 60f;

    [Header("Referências")]
    public Transform cameraContainer;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    private float verticalRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Jogo começou");
        Cursor.lockState = CursorLockMode.Locked; // "some" com o cursor do mouse
    }

    // Update is called once per frame
    void Update()
    {
        // --- Rotação horizontal do Player (eixo Y) ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX, 0f);

        // --- Rotação vertical da Camera (eixo X local) ---
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // --- Movimento WASD / Setas ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * h + transform.forward * v;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // --- Tiro ---
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab,muzzle.position, muzzle.rotation);
        }
    }

}
