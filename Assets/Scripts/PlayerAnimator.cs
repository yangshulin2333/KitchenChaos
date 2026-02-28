using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    // 1. 定义常量，防止拼写错误
    private const string IS_WALKING = "IsWalking";

    // 2. 引用逻辑脚本，用SerializeField暴露给编辑器
    [SerializeField] private Player player;

    // 3. 自身的动画组件
    private Animator animator;

    private void Awake()
    {
        // 4. 缓存组件引用，性能优化关键
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 5. 每帧同步：把逻辑层的状态(IsWalking) 同步给 表现层(Animator)
        // 注意：这里Player类里有个 public bool IsWalking() 方法
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}