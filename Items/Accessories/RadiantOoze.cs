using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class RadiantOoze : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Ooze");
            // Tooltip.SetDefault("You emit light and regen life more quickly at night");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 6, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Main.dayTime)
            {
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1f, 1f, 0.6f);
                player.lifeRegen += 1;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MurkySludge", 5);
            recipe.AddIngredient(null, "PurifiedGel", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}