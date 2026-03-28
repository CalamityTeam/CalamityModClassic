using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedDestroyer : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedMechBoss1;
    public bool CanShowItemDropInUI() => !NPC.downedMechBoss1;
    public string GetConditionDescription() => null;
}