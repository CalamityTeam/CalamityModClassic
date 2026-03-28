using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
    public class OnionSlot : ModAccessorySlot
    {
        public override bool IsEnabled()
        {
            bool active = !Player.active || Main.masterMode;
            return !active && Player.GetModPlayer<CalamityPlayerPreTrailer>().extraAccessoryML;
        }
        public override bool IsHidden() => IsEmpty && !IsEnabled();
    }
}