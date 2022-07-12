using UnityEngine;

public class Movement : MonoBehaviour {
    Rigidbody rb;
    Vector2 rotation = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 move;
        int w, a, s, d;
        if (Input.GetKey("w")) w = 1; else w = 0;
        if (Input.GetKey("a")) a = 1; else a = 0;
        if (Input.GetKey("s")) s = 1; else s = 0;
        if (Input.GetKey("d")) d = 1; else d = 0;
        move.y = w - s;
        move.x = d - a;

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        Camera.main.transform.eulerAngles = new Vector2(rotation.x * 3, transform.eulerAngles.y);
        Camera.main.transform.position = transform.position + new Vector3(0, 1, 0);

        transform.eulerAngles = new Vector2(0, rotation.y * 3);
        rb.velocity = Vector3.Lerp(rb.velocity, transform.TransformDirection(new Vector3(move.x * 7, rb.velocity.y, move.y * 7)), 10f * Time.deltaTime);
    }
}
