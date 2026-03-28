using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedPrime : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedMechBoss3;
    public bool CanShowItemDropInUI() => !NPC.downedMechBoss3;
    public string GetConditionDescription() => null;
}