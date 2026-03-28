using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer;

public class NotDownedMoonLord : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedMoonDude;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedMoonDude;
    public string GetConditionDescription() => null;
}