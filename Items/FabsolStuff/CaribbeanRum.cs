using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class CaribbeanRum : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Caribbean Rum");
			/* Tooltip.SetDefault(@"Boosts life regen by 2 and movement speed and wing flight time by 20%
Makes you floaty and reduces defense by 12
Why is the rum gone?"); */
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
            Item.buffType = Mod.Find<ModBuff>("CaribbeanRum").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 20, 0, 0);
		}
    }
}
