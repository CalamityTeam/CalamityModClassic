using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Perforator
{
    public class BloodyWormTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloody Worm Tooth");
            /* Tooltip.SetDefault("5% increased damage reduction and increased melee stats\n" +
                               "10% increased damage reduction and melee stats when below 50% life"); */
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 15;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.bloodyWormTooth = true;
        }
    }
}