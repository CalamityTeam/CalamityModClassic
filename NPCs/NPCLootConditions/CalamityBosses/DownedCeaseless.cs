using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedCeaseless : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedSentinel1;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedSentinel1;
    public string GetConditionDescription() => null;
}