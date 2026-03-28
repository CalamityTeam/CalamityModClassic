using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Moonshine : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Moonshine");
			/* Tooltip.SetDefault(@"Increases defense by 10 and damage reduction by 5%
Reduces life regen by 1
This stuff is pretty strong but I'm sure you can handle it"); */
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
            Item.buffType = Mod.Find<ModBuff>("Moonshine").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 3, 30, 0);
		}
    }
}
