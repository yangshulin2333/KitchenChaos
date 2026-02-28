using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }
   





    // 1. 定义一个新类，继承自 EventArgs (这是 C# 的规矩，所有事件参数都得认这个祖宗)
    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    // 移动速度
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float runSpeed = 14f;
    // 输入信号来源
    [SerializeField] private GameInput gameInput;
    //【记忆】我最后一次面朝哪里？(为了停下时也能互动)
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    // 当停下来后上一次交互的方向
    private Vector3 lastInteractDir;
    // 【当前锁定】我现在盯着哪个柜子？选定项
    private ClearCounter selectedCounter;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("存在多个玩家实例!");
        }
        Instance = this;

    }


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // 这里的逻辑极其简单：
        // 只要眼睛锁定了东西 (selectedCounter != null)
        // 我就动手 (Interact)
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);

            
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        // 1. 获取输入向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // 2. 转换为三维向量   
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //如果动了，就更新 lastInteractDir（更新记忆）  
        if (moveDir != Vector3.zero) 
        {
            lastInteractDir = moveDir; //最后一次面朝哪里？(为了停下时也能互动)
        }
        // 3. 射线检测距离
        float interactDistance = 2f;

        // 4. 发射射线，检测是否碰撞到台面。哪怕停下来，记忆还在，射线还能射。戴上 countersLayerMask 眼镜，只找柜子。
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCount)) {
                //有碰撞到物体
                if (clearCount != selectedCounter)
                {
                    SetSelectedCounter(clearCount); 
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement() 
    {
        //角色半径是 0.5f
        float playerRadius = .7f;
        float playerHeight = 2f;
        float currentSpeed = gameInput.IsSprinting() ? runSpeed : moveSpeed;
        float moveDistance = currentSpeed * Time.deltaTime;

        // 1. 获取输入向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // 2. 转换为三维向量   
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);


        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
        }
        if (!canMove)
        {
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
            if (canMove)
            {
                moveDir = moveDirZ;
            }
        }

        if (canMove)
        {
            // 2. 执行逻辑：只做一次位移运算
            transform.position += moveDir * moveDistance;
        }
        // 是否在走路
        isWalking = moveDir != Vector3.zero;

        // 3. 转向逻辑
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) 
    {
        this.selectedCounter = selectedCounter;

        // 只有当目标发生改变时，才广播：“喂！我换目标了！”
        OnSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
   

}
