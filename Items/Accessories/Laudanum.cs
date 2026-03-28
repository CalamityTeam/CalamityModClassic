using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class Laudanum : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laudanum");
            /* Tooltip.SetDefault("Boosts your damage by 6%,\n" +
                               "defense by 6, and max movement speed and acceleration by 5%\n" +
                               "Makes you immune to The Horror debuff\n" +
                               "Revengeance drop"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.laudanum = true;
            player.buffImmune[Mod.Find<ModBuff>("Horror").Type] = true;
        }
    }
}