using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedCryogen : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedCryogen;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedCryogen;
    public string GetConditionDescription() => null;
}