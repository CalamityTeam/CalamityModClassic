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
    public class LivingDew : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Living Dew");
            // Tooltip.SetDefault("5% increased damage reduction, +5 defense, and increased life regen while in the Jungle");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneJungle)
            {
                player.lifeRegen += 1;
                player.statDefense += 5;
                player.endurance += 0.05f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ManeaterBulb", 2);
            recipe.AddIngredient(null, "TrapperBulb", 2);
            recipe.AddIngredient(null, "MurkyPaste", 5);
            recipe.AddIngredient(null, "GypsyPowder");
            recipe.AddIngredient(null, "BeetleJuice", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}