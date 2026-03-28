using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories.RareVariants
{
    public class TheEvolution : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Evolution");
            /* Tooltip.SetDefault("You have a 50% chance to reflect projectiles when they hit you back at the enemy for 1000% their original damage\n" +
								"If this effect triggers you get a health regeneration boost for a short time\n" +
								"If the same enemy projectile type hits you again you will resist its damage by 15%"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.projRefRare = true;
        }
	}
}