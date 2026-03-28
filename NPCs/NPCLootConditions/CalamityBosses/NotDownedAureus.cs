using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedAureus : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedAstrageldon;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedAstrageldon;
    public string GetConditionDescription() => null;
}