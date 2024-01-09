using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class TorrentialTear : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Torrential Tear");
			//Tooltip.SetDefault("Summons the rain\nRain will start after some time after this item is used");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item66;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.raining && !Main.slimeRain;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			int num = 86400;
			int num2 = num / 24;
			Main.rainTime = Main.rand.Next(num2 * 8, num);
			if (Main.rand.NextBool(3))
			{
				Main.rainTime += Main.rand.Next(0, num2);
			}
			if (Main.rand.NextBool(4))
			{
				Main.rainTime += Main.rand.Next(0, num2 * 2);
			}
			if (Main.rand.NextBool(5))
			{
				Main.rainTime += Main.rand.Next(0, num2 * 2);
			}
			if (Main.rand.NextBool(6))
			{
				Main.rainTime += Main.rand.Next(0, num2 * 3);
			}
			if (Main.rand.NextBool(7))
			{
				Main.rainTime += Main.rand.Next(0, num2 * 4);
			}
			if (Main.rand.NextBool(8))
			{
				Main.rainTime += Main.rand.Next(0, num2 * 5);
			}
			float num3 = 1f;
			if (Main.rand.NextBool(2))
			{
				num3 += 0.05f;
			}
			if (Main.rand.NextBool(3))
			{
				num3 += 0.1f;
			}
			if (Main.rand.NextBool(4))
			{
				num3 += 0.15f;
			}
			if (Main.rand.NextBool(5))
			{
				num3 += 0.2f;
			}
			Main.rainTime = (int)((float)Main.rainTime * num3);
			Main.raining = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
	}
}