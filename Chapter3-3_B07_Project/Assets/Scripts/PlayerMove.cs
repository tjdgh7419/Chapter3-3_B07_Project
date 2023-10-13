using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform a, b;
    public LineRenderer lineRenderer; // LineRenderer ������Ʈ

    void Start()
    {
        lineRenderer.positionCount = 2; // ������ �� ������ �����˴ϴ�.
        float distance = Vector3.Distance(a.position, b.position);

        // ���� �������� �������� ������ �����մϴ�.
        lineRenderer.SetPosition(0, a.position);
        lineRenderer.SetPosition(1, b.position);
    }

    public float speed = 100f; // ĳ���� �̵� �ӵ�
    public float rotationAngle = 40.0f; // ȸ�� ����

    void Update()
    {
        // ���� �Է��� �޽��ϴ�. ȭ��ǥ Ű �Ǵ� WASD Ű�� ����� �� �ֽ��ϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ������ ����մϴ�.
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;

        // ���� ��ġ�� �̵� ������ ���մϴ�.
        transform.Translate(movement);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationAngle * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
        else if (Input.GetKey(KeyCode.Backspace))
        {
            transform.position += new Vector3(0, -0.1f, 0);
        }
        // ĳ���͸� Ư�� ������ ���ϵ��� ȸ���Ϸ��� �Ʒ� �ּ��� �����ϼ���.
        /*float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
    }
}
