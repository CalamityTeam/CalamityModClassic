using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class CelestialPillarsNotPresent : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.LunarApocalypseIsUp;
    public bool CanShowItemDropInUI() => !NPC.LunarApocalypseIsUp;
    public string GetConditionDescription() => null;
}