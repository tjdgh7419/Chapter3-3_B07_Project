using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform a, b;
    public LineRenderer lineRenderer; // LineRenderer 컴포넌트

    void Start()
    {
        lineRenderer.positionCount = 2; // 라인은 두 점으로 구성됩니다.
        float distance = Vector3.Distance(a.position, b.position);

        // 라인 렌더러에 시작점과 끝점을 설정합니다.
        lineRenderer.SetPosition(0, a.position);
        lineRenderer.SetPosition(1, b.position);
    }

    public float speed = 100f; // 캐릭터 이동 속도
    public float rotationAngle = 40.0f; // 회전 각도

    void Update()
    {
        // 방향 입력을 받습니다. 화살표 키 또는 WASD 키를 사용할 수 있습니다.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향을 계산합니다.
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;

        // 현재 위치에 이동 방향을 더합니다.
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
        // 캐릭터를 특정 방향을 향하도록 회전하려면 아래 주석을 해제하세요.
        /*float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
    }
}
