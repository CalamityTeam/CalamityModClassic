using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedYharon : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedYharon;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedYharon;
    public string GetConditionDescription() => null;
}