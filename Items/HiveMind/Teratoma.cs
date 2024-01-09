using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.HiveMind
{
	public class Teratoma : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Teratoma");
			//Tooltip.SetDefault("Summons the Hive Mind");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Orange;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneCorrupt && !NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMind").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMindP2").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			int num = NPC.NewNPC(player.GetSource_FromThis(), (int)(player.position.X + (float)(Main.rand.Next(-100, 100))), (int)(player.position.Y - 150f), Mod.Find<ModNPC>("HiveMind").Type, 0, 0f, 0f, 0f, 0f, 255);
			if (Main.netMode == NetmodeID.Server && num < 200)
			{
				NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
			}
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 9);
			recipe.AddIngredient(null, "TrueShadowScale", 5);
			recipe.AddIngredient(ItemID.DemoniteBar, 2);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}