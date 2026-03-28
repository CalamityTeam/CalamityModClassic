using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedBetsy : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedBetsy;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedBetsy;
    public string GetConditionDescription() => null;
}