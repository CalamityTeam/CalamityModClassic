using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedCultist : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedAncientCultist;
    public bool CanShowItemDropInUI() => !NPC.downedAncientCultist;
    public string GetConditionDescription() => null;
}