using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedDoG : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.downedDoG;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.downedDoG;
    public string GetConditionDescription() => null;
}