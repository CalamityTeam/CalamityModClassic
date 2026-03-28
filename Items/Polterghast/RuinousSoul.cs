using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Polterghast
{
    public class RuinousSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruinous Soul");
            // Tooltip.SetDefault("A shard of the distant past");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 7, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
    }
}