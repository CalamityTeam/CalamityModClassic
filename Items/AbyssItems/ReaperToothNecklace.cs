using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.AbyssItems
{
    public class ReaperToothNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaper Tooth Necklace");
            /* Tooltip.SetDefault("Increases armor penetration by 100\n" +
                "Increases all damage by 25%\n" +
                "Cuts your defense and damage reduction in half"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.reaperToothNecklace = true;
            player.GetArmorPenetration(DamageClass.Generic) += 100;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ReaperTooth", 6);
            recipe.AddIngredient(null, "Lumenite", 15);
            recipe.AddIngredient(null, "DepthCells", 15);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}