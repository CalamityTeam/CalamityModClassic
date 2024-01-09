using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassic1Point2.Items
{
	public class OmegaHealingPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Omega Healing Potion");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.healLife = 500;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = 100000;
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

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SupremeHealingPotion");
			recipe.AddIngredient(null, "BloodOrb", 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}