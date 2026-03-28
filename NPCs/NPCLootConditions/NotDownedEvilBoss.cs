using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedEvilBoss : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedWhar;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedWhar;
    public string GetConditionDescription() => null;
}