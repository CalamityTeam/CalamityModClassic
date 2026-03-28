using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer;

public class NotDownedKS : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedSlimeKing;
    public bool CanShowItemDropInUI() => !NPC.downedSlimeKing;
    public string GetConditionDescription() => null;
}