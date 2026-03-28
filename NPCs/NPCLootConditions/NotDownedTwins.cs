using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer;

public class NotDownedTwins : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedMechBoss2;
    public bool CanShowItemDropInUI() => !NPC.downedMechBoss2;
    public string GetConditionDescription() => null;
}