using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.AbyssItems
{
	public class SunkenStew : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sunken Stew");
            /* Tooltip.SetDefault("Causes potion sickness for 50 (37 with Philosopher's Stone effect) seconds instead of 60\n" +
                "Restores 120 life and 150 mana"); */
        }
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
            Item.useAnimation = 17;
			Item.useTime = 17;
            Item.rare = 3;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 216000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

        public override bool CanUseItem(Player player)
        {
            return player.FindBuffIndex(BuffID.PotionSickness) == -1;
        }

        public override bool ConsumeItem(Player player)
        {
            return player.FindBuffIndex(BuffID.PotionSickness) == -1;
        }

        public override void OnConsumeItem(Player player)
        {
            player.statLife += 120;
            player.statMana += 150;
            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
            if (player.statMana > player.statManaMax2)
            {
                player.statMana = player.statManaMax2;
            }
            player.AddBuff(BuffID.ManaSickness, Player.manaSickTime, true);
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(120, true);
                player.ManaEffect(150);
            }
            player.AddBuff(BuffID.WellFed, 216000);
            player.AddBuff(BuffID.PotionSickness, (player.pStone ? 2220 : 3000));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DepthCells", 5);
            recipe.AddIngredient(ItemID.GlowingMushroom, 3);
            recipe.AddIngredient(ItemID.Honeyfin);
            recipe.AddIngredient(ItemID.Bowl);
            recipe.AddTile(TileID.CookingPots);
			recipe.Register();
		}
	}
}