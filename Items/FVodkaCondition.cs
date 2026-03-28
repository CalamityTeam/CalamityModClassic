using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class FVodkaCondition : IItemDropRuleCondition
    {
        Player player = Main.LocalPlayer;
        public bool CanDrop(DropAttemptInfo info) => player.GetModPlayer<CalamityPlayerPreTrailer>().fabsolVodka;
        
        public bool CanShowItemDropInUI() => player.GetModPlayer<CalamityPlayerPreTrailer>().fabsolVodka;
        
        public string GetConditionDescription() => "While Fabsol's Vodka is active";
    }
}