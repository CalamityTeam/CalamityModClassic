using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedProvidence : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedProvidence;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedProvidence;
    public string GetConditionDescription() => null;
}