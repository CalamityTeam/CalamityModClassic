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
    public class StatisCurse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statis' Curse");
            /* Tooltip.SetDefault("Increased max minions by 3 and 10% increased minion damage\n" +
                "Increased minion knockback\n" +
                "Grants shadowflame powers to all minions\n" +
                "Minions make enemies cry on hit"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.shadowMinions = true;
            modPlayer.tearMinions = true;
            player.GetKnockback(DamageClass.Summon).Base += 2.5f;
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentStardust, 10);
            recipe.AddIngredient(null, "StatisBlessing");
            recipe.AddIngredient(null, "TheFirstShadowflame");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}