using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }



    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        //在转移之前，必须确认目标柜台是空的。这是防止逻辑覆盖（Overwrite）的关键防御性编程。
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("柜台已经有了一个厨房物体!");
        }

        //因为物品刚生成或者被丢弃时可能没有主人。如果有旧主人，必须调用它的 Clear 方法，把旧主人的 kitchenObject 变量置为 null。这叫“解绑”。
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }


        this.kitchenObjectParent = kitchenObjectParent;

        
        kitchenObjectParent.SetKitchenObject(this);


        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }


    public void DestroySelf()
    {
        GetKitchenObjectParent().ClearKitchenObject();
        Destroy(gameObject);
    }



    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject =  kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

}
