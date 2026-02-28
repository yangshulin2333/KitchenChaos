using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update() 
    {
        

        if (testing && Input.GetKeyDown(KeyCode.T)){
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter); //把这个物体从这个台面上拿走，放到另一个台面上
            }
        }
    }
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            //把物品交给玩家
            //kitchenObject.SetClearCounter(player);
            Debug.Log("台面上已有物体，无法放置新物体" + kitchenObject.GetClearCounter());
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
