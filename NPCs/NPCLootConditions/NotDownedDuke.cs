using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedDuke : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedFishron;
    public bool CanShowItemDropInUI() => !NPC.downedFishron;
    public string GetConditionDescription() => null;
}