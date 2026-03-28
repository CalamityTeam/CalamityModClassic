using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedPlantera : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedPlantBoss;
    public bool CanShowItemDropInUI() => !NPC.downedPlantBoss;
    public string GetConditionDescription() => null;
}