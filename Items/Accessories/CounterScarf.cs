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
    public class CounterScarf : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Counter Scarf");
            /* Tooltip.SetDefault("Melee weapons that don't fire projectiles are granted 20% more damage\n" +
                "Grants the ability to dash; dashing into an attack will cause you to dodge it\n" +
                "After a dodge you will be granted a buff to all damage, melee speed, and all crit chance for a short time\n" +
                "After a successful dodge you must wait 15 seconds before you can dodge again\n" +
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
            modPlayer.dodgeScarf = true;
            modPlayer.dashMod = 1;
        }
    }
}