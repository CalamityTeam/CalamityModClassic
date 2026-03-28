using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class EssenceofCinder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Essence of Sunlight");
			// Tooltip.SetDefault("The essence of sky, light, and storm creatures");
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			maxFallSpeed = 0f;
			float num = (float)Main.rand.Next(90, 111) * 0.01f;
			num *= Main.essScale;
			Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.3f * num, 0.3f * num, 0.05f * num);
		}

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 5;
		}
	}
}