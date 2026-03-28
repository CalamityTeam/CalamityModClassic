using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Vodka : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vodka");
			/* Tooltip.SetDefault(@"Boosts damage by 6% and critical strike chance by 2%
Reduces life regen by 1 and defense by 4
The number one alcohol for creating great mixed drinks"); */
		}

		public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("Vodka").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 3, 30, 0);
		}
    }
}
