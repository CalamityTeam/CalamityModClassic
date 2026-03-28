using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedPBG : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedPlaguebringer;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedPlaguebringer;
    public string GetConditionDescription() => null;
}