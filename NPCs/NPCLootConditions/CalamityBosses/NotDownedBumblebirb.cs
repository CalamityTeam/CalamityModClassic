using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class NotDownedBumblebirb : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => !CalamityWorldPreTrailer.downedBumble;
    public bool CanShowItemDropInUI() => !CalamityWorldPreTrailer.downedBumble;
    public string GetConditionDescription() => null;
}