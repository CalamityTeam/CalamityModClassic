using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedStormWeaver : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedSentinel2;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedSentinel2;
    public string GetConditionDescription() => null;
}