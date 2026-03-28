using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedRavager : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedScavenger;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedScavenger;
    public string GetConditionDescription() => null;
}