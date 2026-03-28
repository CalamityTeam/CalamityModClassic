using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedAquaticScourge : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedAquaticScourge;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedAquaticScourge;
    public string GetConditionDescription() => null;
}