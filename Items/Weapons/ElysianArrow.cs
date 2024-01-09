using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class ElysianArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Elysian Arrow");
			//Tooltip.SetDefault("Summons meteors from the sky on death");
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 3f;
			Item.value = 2000;
			Item.shoot = Mod.Find<ModProjectile>("ElysianArrow").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 40;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(null, "UnholyEssence");
			recipe.AddIngredient(ItemID.HolyArrow, 150);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}