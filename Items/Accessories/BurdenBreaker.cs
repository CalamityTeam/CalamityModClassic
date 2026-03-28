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
    public class BurdenBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Determination Breaker");
            /* Tooltip.SetDefault("The bad time\n" +
                "Removes immunity frames\n" +
                "If you want a crazy challenge, equip this\n" +
                "Touching lava kills you instantly"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = false;
            if (Collision.LavaCollision(player.position, player.width, (player.waterWalk ? (player.height - 6) : player.height)))
            {
                player.GetModPlayer<CalamityPlayerPreTrailer>().KillPlayer();
            }
            else if (player.immune)
            {
                player.immune = false;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddIngredient(RecipeGroupID.IronBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}