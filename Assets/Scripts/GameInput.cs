using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        

        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;



    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();



        //保证斜方向移动速度不变
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool IsSprinting() 
    {
        // 读取 Sprint 动作的数值。如果是按钮，它返回 float (0 或 1)。
        // 这里我们可以简单地判断：如果读到的值 > 0，就是按下了。
        return playerInputActions.Player.Sprint.ReadValue<float>() > 0f;
    }
}
