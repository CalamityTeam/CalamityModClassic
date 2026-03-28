using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class DownedGolem : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedGolemBoss;
    public bool CanShowItemDropInUI() => NPC.downedGolemBoss;
    public string GetConditionDescription() => null;
}