using Fusion;
using TMPro;
using UnityEngine;


public class OrderObject : NetworkBehaviour
{
    
    [Networked, OnChangedRender(nameof(OnTextChanged))]
    public NetworkString<_16> OrderTextValue { get; set; }
    
    [SerializeField] private TMP_Text _orderText;
    [SerializeField] private int _orderNumber;

    
    public TMP_Text OrderText
    {
        get => _orderText;
        set => _orderText = value;
    }

    public int OrderNumber
    {
        get => _orderNumber;
        set => _orderNumber = value;
    }

    public void Init(string orderText, int orderNumber)
    {
        OrderTextValue = orderText;
        _orderNumber = orderNumber;
    }

    public override void Spawned()
    {
        base.Spawned();
        OnTextChanged();
    }

    private void OnTextChanged()
    {
        _orderText.text = OrderTextValue.Value;
    }
}
