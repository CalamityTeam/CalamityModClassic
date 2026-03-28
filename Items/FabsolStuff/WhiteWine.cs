using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class WhiteWine : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("White Wine");
			/* Tooltip.SetDefault(@"Boosts magic damage by 10%
Reduces defense by 6 and life regen by 1
I drank a full barrel of this stuff once in one night, I couldn't remember who I was the next day"); */
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
            Item.healMana = 400;
            Item.buffType = Mod.Find<ModBuff>("WhiteWine").Type;
            Item.buffTime = 10800; //3 minutes
            Item.value = Item.buyPrice(0, 16, 60, 0);
		}
    }
}
