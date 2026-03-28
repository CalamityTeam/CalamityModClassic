using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
    public class HadarianMembrane : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hadarian Membrane");
            // Tooltip.SetDefault("The membrane of an astral creature's wings");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 4, 50, 0);
			Item.rare = 7;
        }
    }
}