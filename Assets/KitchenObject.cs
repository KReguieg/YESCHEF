using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectSO;

    public KitchenObjectScriptableObject KitchenObjectSo => _kitchenObjectSO;
}
