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
	public class Grax : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Grax");
			//Tooltip.SetDefault("Hitting an enemy will greatly boost your defense and melee stats for a short time");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 350;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 5;
			Item.useTurn = true;
			Item.axe = 50;
			Item.hammer = 200;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
			Item.value = 5000000;
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
			recipe.AddIngredient(null, "FellerofEvergreens");
			recipe.AddIngredient(null, "DraedonBar", 5);
			recipe.AddRecipeGroup("LunarAxe");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			player.AddBuff(Mod.Find<ModBuff>("GraxDefense").Type, 480);
		}
	}
}
