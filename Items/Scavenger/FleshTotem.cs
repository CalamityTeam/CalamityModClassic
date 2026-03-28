using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Scavenger
{
    public class FleshTotem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flesh Totem");
            /* Tooltip.SetDefault("Halves enemy contact damage\n" +
                "When you take contact damage this effect has a 20 second cooldown"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = 8;
            Item.value = Item.buyPrice(0, 24, 0, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.fleshTotem = true;
        }
    }
}