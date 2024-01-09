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
	public class GreatswordofBlah : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 110;
			Item.damage = 350;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 110;
			Item.value = 10000000;
			Item.shoot = Mod.Find<ModProjectile>("JudgementBlah").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(108, 45, 199);
	            }
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GreatswordofJudgement");
			recipe.AddIngredient(null, "NightmareFuel", 5);
        	recipe.AddIngredient(null, "EndothermicEnergy", 5);
			recipe.AddIngredient(null, "HellcasterFragment", 5);
			recipe.AddIngredient(null, "DarksunFragment", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	}
}
