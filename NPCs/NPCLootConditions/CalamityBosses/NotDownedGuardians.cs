using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedGuardians : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedGuardians;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedGuardians;
    public string GetConditionDescription() => null;
}