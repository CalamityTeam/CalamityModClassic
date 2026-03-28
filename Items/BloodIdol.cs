using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace CalamityModClassicPreTrailer.Items
{
	public class BloodIdol : ModItem
	{
		public override void SetStaticDefaults()
	 	{
	 		// DisplayName.SetDefault("Blood Relic");
	 		// Tooltip.SetDefault("Summons a blood moon");
	 	}
	
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item66;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.bloodMoon && !Main.dayTime;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            Main.bloodMoon = true;
            if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "BloodlettingEssence", 2);
			recipe.AddIngredient(null, "UnholyCore", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "FetidEssence", 2);
            recipe.AddIngredient(null, "UnholyCore", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}