using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedPolterghast : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedPolterghast;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedPolterghast;
    public string GetConditionDescription() => null;
}