using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class Cosmolight : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmolight");
			//Tooltip.SetDefault("Changes night to day and vice versa");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (!Main.dayTime)
			{
				Main.time = 0.0;
				Main.dayTime = true;
    		}
    		else
    		{
    			Main.time = 0.0;
				Main.dayTime = false;
				Main.moonPhase++;
				if (Main.moonPhase >= 8)
				{
					Main.moonPhase = 0;
				}
    		}
    		if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Daylight");
			recipe.AddIngredient(null, "Moonlight");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}