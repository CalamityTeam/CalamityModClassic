using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class EvergreenGin : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Evergreen Gin");
			/* Tooltip.SetDefault(@"Boosts nature-based weapon damage by 15% and damage reduction by 5%
Reduces life regen by 1
It tastes like a Christmas tree if you can imagine that"); */
		}

		public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = 4;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("EvergreenGin").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 16, 60, 0);
		}
    }
}
