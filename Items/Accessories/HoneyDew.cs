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
    public class HoneyDew : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Honey Dew");
            /* Tooltip.SetDefault("5% increased damage reduction, +5 defense, and increased life regen while in the Jungle\n" +
            "Poison and Venom immunity\n" +
            "Honey-like life regen with no speed penalty\n" +
            "Most bee/hornet enemies and projectiles do 75% damage to you"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 7;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.beeResist = true;
            if (player.ZoneJungle)
            {
                player.lifeRegen += 1;
                player.statDefense += 5;
                player.endurance += 0.05f;
            }
            player.buffImmune[70] = true;
            player.buffImmune[20] = true;
            if (!player.honey && player.lifeRegen < 0)
            {
                player.lifeRegen += 2;
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
            }
            player.lifeRegenTime += 1;
            player.lifeRegen += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "LivingDew");
            recipe.AddIngredient(ItemID.BottledHoney, 10);
            recipe.AddIngredient(ItemID.BeeWax, 10);
            recipe.AddIngredient(ItemID.Bezoar);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}