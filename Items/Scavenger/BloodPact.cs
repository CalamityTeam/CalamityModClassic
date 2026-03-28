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
    public class BloodPact : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Pact");
            /* Tooltip.SetDefault("Doubles your max HP\n" +
                "Allows you to be critically hit 25% of the time"); */
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
            modPlayer.bloodPact = true;
        }
    }
}