using Fusion;
using UnityEngine;

public class KitchenObject : NetworkBehaviour
{
	[SerializeField] private KitchenObjectScriptableObject _kitchenObjectSO;

	public KitchenObjectScriptableObject KitchenObjectSo => _kitchenObjectSO;

	//public void Init()
	//{
	//}
}
