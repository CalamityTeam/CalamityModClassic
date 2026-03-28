using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedCrabulon : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedCrabulon;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedCrabulon;
    public string GetConditionDescription() => null;
}