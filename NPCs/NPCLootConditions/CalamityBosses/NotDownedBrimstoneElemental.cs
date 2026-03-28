using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedBrimstoneElemental : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedBrimstoneElemental;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedBrimstoneElemental;
    public string GetConditionDescription() => null;
}