using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer;

public class DownedMoonLord : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedMoonlord;
    public bool CanShowItemDropInUI() => NPC.downedMoonlord;
    public string GetConditionDescription() => null;
}