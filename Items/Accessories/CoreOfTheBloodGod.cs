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
    public class CoreOfTheBloodGod : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core of the Blood God");
            /* Tooltip.SetDefault("5% increased damage reduction\n" +
                "7% increased damage\n" +
                "When below 100 defense you gain 15% increased damage\n" +
                "Halves enemy contact damage\n" +
                "When you take contact damage this effect has a 20 second cooldown\n" +
                "Boosts your max HP by 10%"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.coreOfTheBloodGod = true;
            modPlayer.fleshTotem = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodyWormScarf");
            recipe.AddIngredient(null, "BloodPact");
            recipe.AddIngredient(null, "FleshTotem");
            recipe.AddIngredient(null, "BloodflareCore");
            recipe.AddIngredient(null, "CosmiliteBar", 5);
            recipe.AddIngredient(null, "Phantoplasm", 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}