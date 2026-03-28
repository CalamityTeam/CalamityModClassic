using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class DownedAstrumDeus : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.downedStarGod;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.downedStarGod;
    public string GetConditionDescription() => null;
}