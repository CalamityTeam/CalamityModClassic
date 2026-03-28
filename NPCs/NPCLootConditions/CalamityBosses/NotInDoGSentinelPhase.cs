using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotInDoGSentinelPhase : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0;
    public string GetConditionDescription() => null;
}