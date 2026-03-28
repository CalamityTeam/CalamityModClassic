using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedEye : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedBoss1;
    public bool CanShowItemDropInUI() => !NPC.downedBoss1;
    public string GetConditionDescription() => null;
}