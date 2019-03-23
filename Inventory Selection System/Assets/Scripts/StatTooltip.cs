using System.Text;
using UnityEngine.UI;
using CharacterStats;
using UnityEngine;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] Text StatNameText;
    [SerializeField] Text StatModifierLabelText;
    [SerializeField] Text StatModifiersText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName)
    {
        StatNameText.text = GetStatTopText(stat, statName);
        StatModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);
        sb.Append(" (");
        sb.Append(stat.BaseValue);

        if (stat.Value > stat.BaseValue)
            sb.Append("+");

        sb.Append(stat.Value - stat.BaseValue);
        sb.Append(")");
        return sb.ToString();
    }
    

    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;
        foreach (StatModifier mod in stat.StatModifiers)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
        
            if (mod.Value > 0)
                sb.Append("+");

            sb.Append(mod.Value);

            EquippableItem item = mod.Source as EquippableItem;

            if(item != null)
            {
                sb.Append("  ");
                sb.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifier is not an EquippableItem");
            }
        }
        return sb.ToString();
    }
}
