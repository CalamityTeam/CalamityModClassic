using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedCalDoppel : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedCalamitas;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedCalamitas;
    public string GetConditionDescription() => null;
}