using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }



    public void SetClearCounter(ClearCounter clearCounter)
    {

        //在转移之前，必须确认目标柜台是空的。这是防止逻辑覆盖（Overwrite）的关键防御性编程。
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("柜台已经有了一个厨房物体!");
        }

        //因为物品刚生成或者被丢弃时可能没有主人。如果有旧主人，必须调用它的 Clear 方法，把旧主人的 kitchenObject 变量置为 null。这叫“解绑”。
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }


        this.clearCounter = clearCounter;

        
        clearCounter.SetKitchenObject(this);


        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }




}
