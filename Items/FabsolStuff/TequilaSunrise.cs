using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class TequilaSunrise : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tequila Sunrise");
			/* Tooltip.SetDefault(@"Boosts damage, damage reduction, and knockback by 7%, crit chance by 3%, and defense by 15 during daytime
Reduces life regen by 1
The greatest daytime drink I've ever had"); */
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
            Item.buffType = Mod.Find<ModBuff>("TequilaSunrise").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 20, 0, 0);
		}
    }
}
