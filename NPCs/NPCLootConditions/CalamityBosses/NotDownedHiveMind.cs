using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedHiveMind : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedHiveMind;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedHiveMind;
    public string GetConditionDescription() => null;
}