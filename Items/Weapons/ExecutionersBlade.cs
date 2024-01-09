using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class ExecutionersBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Executioner's Blade");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 260;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 3;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6.75f;
			Item.UseSound = SoundID.Item73;
			Item.autoReuse = true;
			Item.height = 50;
			Item.value = 1350000;
			Item.shoot = Mod.Find<ModProjectile>("ExecutionersBlade").Type;
			Item.shootSpeed = 26f;
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
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CosmiliteBar", 11);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}
