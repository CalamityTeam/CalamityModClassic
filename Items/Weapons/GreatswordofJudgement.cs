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
	public class GreatswordofJudgement : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 70;
			Item.damage = 100;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
			Item.value = 5000000;
			Item.shoot = Mod.Find<ModProjectile>("Judgement").Type;
			Item.shootSpeed = 10f;
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
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 11);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
