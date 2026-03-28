using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Everclear : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Everclear");
			/* Tooltip.SetDefault(@"Boosts damage by 25%
Reduces life regen by 10 and defense by 40
This is the most potent booze I have, be careful with it"); */
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
            Item.buffType = Mod.Find<ModBuff>("Everclear").Type;
            Item.buffTime = 900; //15 seconds
            Item.value = Item.buyPrice(0, 6, 60, 0);
		}
    }
}
