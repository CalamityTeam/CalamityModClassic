using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class Cosmolight : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmolight");
			// Tooltip.SetDefault("Changes night to day and vice versa");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

        public override bool CanUseItem(Player player)
        {
            return !CalamityGlobalNPC.AnyBossNPCS();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			Main.time = 0.0;
			Main.dayTime = !Main.dayTime;
			if (Main.dayTime)
			{
				if (++Main.moonPhase >= 8)
				{
					Main.moonPhase = 0;
				}
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
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