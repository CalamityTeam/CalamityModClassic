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
    public class Regenator : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Regenator");
			// Tooltip.SetDefault("Reduces max HP by 50% but greatly improves life regeneration");
		}

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 5;
            Item.defense = 6;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.regenator = true;
		}
	}
}