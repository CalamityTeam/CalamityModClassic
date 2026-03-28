using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class DeliciousMeat : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Delicious Meat");
            // Tooltip.SetDefault("Minor improvements to all stats\n'So very delicious'");
        }
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 30;
			Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = 5;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item2;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 108000;
        }
    }
}
