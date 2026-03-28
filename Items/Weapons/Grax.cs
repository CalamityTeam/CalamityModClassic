using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Grax : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grax");
			// Tooltip.SetDefault("Hitting an enemy will greatly boost your defense and melee stats for a short time");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 450;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.useTime = 5;
			Item.useTurn = true;
			Item.axe = 50;
			Item.hammer = 200;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "FellerofEvergreens");
			recipe.AddIngredient(null, "DraedonBar", 5);
			recipe.AddRecipeGroup("LunarHamaxe");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			player.AddBuff(Mod.Find<ModBuff>("GraxDefense").Type, 480);
		}
	}
}
