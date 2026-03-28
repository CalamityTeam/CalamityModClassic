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
    public class StatisBlessing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statis' Blessing");
            /* Tooltip.SetDefault("Increased max minions by 3 and 10% increased minion damage\n" +
                "Increased minion knockback\n" +
                "Minions cause enemies to cry on hit"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 45, 0, 0);
            Item.rare = 9;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.tearMinions = true;
            player.GetKnockback(DamageClass.Summon).Base += 2.5f;
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PapyrusScarab);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(null, "CoreofCinder", 5);
            recipe.AddIngredient(ItemID.HolyWater, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}