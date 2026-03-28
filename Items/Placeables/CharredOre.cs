using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class CharredOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charred Ore");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("CharredOre").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 6;
        }
    }
}