using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedCalDoppelorPlantera : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas;
    public bool CanShowItemDropInUI() => NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas;
    public string GetConditionDescription() => null;
}