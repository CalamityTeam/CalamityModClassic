using Terraria;
using Terraria.GameContent.ItemDropRules;
namespace CalamityModClassicPreTrailer.NPCs.Yharon;

public class DarkSunCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.downedBuffedMothron;
    public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.downedBuffedMothron;
    public string GetConditionDescription() => "After defeating Mothron in the buffed Solar Eclipse";
}