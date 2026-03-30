using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.GetComponent<Transform>().name);
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
    }
}
