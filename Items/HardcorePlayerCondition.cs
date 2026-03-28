using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items;

public class HardcorePlayerCondition : IItemDropRuleCondition
{
    Player player = Main.LocalPlayer;
    public bool CanDrop(DropAttemptInfo info) => player.difficulty == 2;
    public bool CanShowItemDropInUI() => player.difficulty == 2;
    public string GetConditionDescription() =>"If playing with a Hardcore character";
}