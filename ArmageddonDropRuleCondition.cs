using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer;

public class ArmageddonDropRuleCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.armageddon;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.armageddon;
    public string GetConditionDescription() => null;
}