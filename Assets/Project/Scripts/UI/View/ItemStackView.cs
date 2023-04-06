using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

public class ItemStackView : DraggableItem<InventorySlotView>
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;

    public ItemStack itemStack { get; private set; }

    public void Init(ItemStack stack)
    {
        itemStack = stack;
    }
    
    public void UpdateView()
    {
        Debug.Log(itemStack.data);
        _icon.sprite = itemStack.data.icon;
        _countText.text = itemStack.count > 1 ? itemStack.count.ToString() : "";
    }
}
