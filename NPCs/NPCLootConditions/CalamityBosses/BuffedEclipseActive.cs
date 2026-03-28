using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class BuffedEclipseActive : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.buffedEclipse;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.buffedEclipse;
    public string GetConditionDescription() => null;
}