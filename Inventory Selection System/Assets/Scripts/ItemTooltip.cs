using UnityEngine.UI;
using UnityEngine;
using System.Text;

public class ItemTooltip : MonoBehaviour
{

    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemClassText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();
        ItemClassText.text = item.EquipmentClass.ToString();

        sb.Length = 0;
        AddStat(item.Strength, "Strength");
        AddStat(item.Agility, "Agility");
        AddStat(item.Intelligence, "Intelligence");
        AddStat(item.Defence, "Defence");
        AddStat(item.Damage, "Damage");

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string StatName)
    {
        if(value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();
            if (value > 0)
                sb.Append("+");
            sb.Append(value);
            sb.Append(" ");
            sb.Append(StatName);

        }
    }
}

