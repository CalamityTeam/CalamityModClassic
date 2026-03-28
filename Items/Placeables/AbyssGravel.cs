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
    public class AbyssGravel : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyss Gravel");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("AbyssGravel").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = 999;
        }
    }
}