using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Yharon
{
	public class ChickenEgg : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Dragon Egg");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Summons the loyal guardian of the tyrant king\nIt yearns for the jungle");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("YharonSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneJungle;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BarofLife", 5);
			recipe.AddIngredient(null, "GalacticaSingularity");
			recipe.AddIngredient(null, "CosmiliteBar", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}