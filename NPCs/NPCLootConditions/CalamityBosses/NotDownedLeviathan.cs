using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedLeviathan : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedLeviathan;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedLeviathan;
    public string GetConditionDescription() => null;
}