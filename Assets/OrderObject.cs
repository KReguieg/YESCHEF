using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OrderObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _orderText;

    public TMP_Text OrderText
    {
        get => _orderText;
        set => _orderText = value;
    }

    public void Init(string orderText)
    {
        _orderText.text = orderText;
    }
}
