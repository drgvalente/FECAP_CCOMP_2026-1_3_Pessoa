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
    public Transform gunContainer;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    private float verticalRotation = 0f;
    private Rigidbody rb; // referência ao Rigidbody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // "some" com o cursor do mouse
        rb = GetComponent<Rigidbody>(); // obtém o Rigidbody
        rb.freezeRotation = true;       // impede o Rigidbody de rotacionar sozinho
    }

    // Update is called once per frame
    void Update()
    {
        // --- Rotação horizontal do Player (eixo Y) ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX, 0f);

        // --- Rotação vertical da Camera e Arma (eixo X local) ---
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        gunContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // --- Movimento WASD / Setas ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * h + transform.forward * v;
        //transform.position += direction * moveSpeed * Time.deltaTime;
        // usa velocidade do Rigidbody ao invés de transform.position
        // O eixo Y é preservado para que a gravidade continue funcionando normalmente
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);


        // --- Tiro ---
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab,muzzle.position, muzzle.rotation);
        }
    }

}
