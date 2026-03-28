using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedDS : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.downedDesertScourge;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.downedDesertScourge;
    public string GetConditionDescription() => null;
}