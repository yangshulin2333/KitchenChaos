using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]private float moveSpeed = 7f;
    private void Update()
    {
        #region 摁键输入，向量分离
        Vector2 inputVector = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        #endregion

        #region 实际移动
        //保证斜方向移动速度不变
        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir  *Time.deltaTime * moveSpeed;

        //移动方向,平滑转向
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir,Time.deltaTime * rotationSpeed);
        #endregion

    }
}
