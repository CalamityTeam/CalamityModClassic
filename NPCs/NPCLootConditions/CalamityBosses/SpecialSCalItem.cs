using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class SpecialSCalItem : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 3;
    public bool CanShowItemDropInUI() => Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 3;
    public string GetConditionDescription() => null;
}