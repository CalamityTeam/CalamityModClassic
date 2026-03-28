using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedSignus : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedSentinel3;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedSentinel3;
    public string GetConditionDescription() => null;
}