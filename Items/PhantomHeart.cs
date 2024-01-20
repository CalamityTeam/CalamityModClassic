using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items
{
	public class PhantomHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phantom Heart");
			//Tooltip.SetDefault("Permanently increases maximum mana by 100");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item29;
			Item.consumable = true;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (modPlayer.pHeart)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				if (Main.myPlayer == player.whoAmI)
				{
					player.ManaEffect(100);
				}
				CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
				modPlayer.pHeart = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Phantoplasm", 150);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}