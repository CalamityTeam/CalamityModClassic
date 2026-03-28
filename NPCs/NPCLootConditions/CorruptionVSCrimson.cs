using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class CorruptionVSCrimson : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => WorldGen.crimson;
    public bool CanShowItemDropInUI() => WorldGen.crimson;
    public string GetConditionDescription() => null;
}