using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VictideLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Victide Leggings");
            /* Tooltip.SetDefault("Movement speed increased by 8%\n" +
                "Speed greatly increased while submerged in liquid"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 0, 50, 0);
			Item.rare = 2;
            Item.defense = 4; //9
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f;
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                player.moveSpeed += 0.5f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VictideBar", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}