using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedSCal : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedSCal;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedSCal;
    public string GetConditionDescription() => null;
}