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
    public class DeepDiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deep Diver");
            /* Tooltip.SetDefault("15% increased damage, defense, and movement speed when underwater\n" +
								"While underwater you gain the ability to dash great distances"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 2;
            Item.defense = 2;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
				CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
				modPlayer.deepDiver = true;
				modPlayer.dashMod = 5;
            }
        }
	}
}