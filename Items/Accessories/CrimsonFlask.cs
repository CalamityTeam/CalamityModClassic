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
    public class CrimsonFlask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Flask");
            // Tooltip.SetDefault("7% increased damage reduction and +3 defense while in the crimson");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneCrimson)
            {
                player.statDefense += 3;
                player.endurance += 0.07f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodlettingEssence", 3);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}