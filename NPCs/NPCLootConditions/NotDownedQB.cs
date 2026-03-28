using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedQB : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedQueenBee;
    public bool CanShowItemDropInUI() => !NPC.downedQueenBee;
    public string GetConditionDescription() => null;
}