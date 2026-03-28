using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NoDownedMechBosses : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !NPC.downedMechBossAny;
    public bool CanShowItemDropInUI() => !NPC.downedMechBossAny;
    public string GetConditionDescription() => null;
}