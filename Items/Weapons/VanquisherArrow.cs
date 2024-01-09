using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class VanquisherArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Vanquisher Arrow");
			//Tooltip.SetDefault("Chases enemies and pierces through tiles");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 2250;
			Item.shoot = Mod.Find<ModProjectile>("VanquisherArrow").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 40;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "CosmiliteBar");
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}