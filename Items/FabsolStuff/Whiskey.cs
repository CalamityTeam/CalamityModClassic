using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Whiskey : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Whiskey");
			/* Tooltip.SetDefault(@"Boosts damage and knockback by 4% and critical strike chance by 2%
Reduces defense by 8
The burning sensation makes it tastier"); */
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
            Item.buffType = Mod.Find<ModBuff>("Whiskey").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 3, 30, 0);
		}
    }
}
