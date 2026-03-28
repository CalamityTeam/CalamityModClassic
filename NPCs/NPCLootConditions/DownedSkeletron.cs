using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class DownedSkeletron : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
    public bool CanShowItemDropInUI() => NPC.downedBoss3;
    public string GetConditionDescription() => null;
}