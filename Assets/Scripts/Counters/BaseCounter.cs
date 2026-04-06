using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player){
        Debug.LogError("基类柜台永远不会被触发");
    }
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("基类柜台切菜替代交互永远不会被触发");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
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
