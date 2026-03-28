using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
	public class DarksunFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Darksun Fragment");
			// Tooltip.SetDefault("A shard of lunar and solar energy");
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			maxFallSpeed = 0f;
			float num = (float)Main.rand.Next(90, 111) * 0.01f;
			num *= Main.essScale;
			Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.5f * num, 0.5f * num, 0.5f * num);
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 8, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}
	}
}