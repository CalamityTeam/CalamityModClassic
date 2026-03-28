using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedPerforators : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedPerforator;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedPerforator;
    public string GetConditionDescription() => null;
}