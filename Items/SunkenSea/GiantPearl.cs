using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.SunkenSea
{
    public class GiantPearl : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Giant Pearl");
            /* Tooltip.SetDefault("You have a light aura around you\n" +
                "Enemies within the aura are slowed down\n" +
				"Does not work on bosses"); */
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 32;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 2;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.giantPearl = true;
			Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.45f, 0.8f, 0.8f);
        }
    }
}