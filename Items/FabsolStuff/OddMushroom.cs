using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class OddMushroom : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Odd Mushroom");
            // Tooltip.SetDefault("Trippy");
        }
		
		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 48;
			Item.useTurn = true;
			Item.maxStack = 30;
            Item.useAnimation = 17;
			Item.useTime = 17;
            Item.rare = 3;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("Trippy").Type;
            Item.buffTime = 216000;
			Item.value = Item.buyPrice(1, 0, 0, 0);
		}
	}
}