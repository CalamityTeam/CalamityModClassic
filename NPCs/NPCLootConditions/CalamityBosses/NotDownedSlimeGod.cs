using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedSlimeGod : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedSlimeGod;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedSlimeGod;
    public string GetConditionDescription() => null;
}