using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class NotDownedGolem : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedGolemBaby;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedGolemBaby;
    public string GetConditionDescription() => null;
}