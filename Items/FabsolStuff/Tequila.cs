using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Tequila : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tequila");
			/* Tooltip.SetDefault(@"Boosts damage, damage reduction, and knockback by 3%, crit chance by 2%, and defense by 5 during daytime
Reduces life regen by 1
Great for mixing up daytime drinks"); */
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
            Item.buffType = Mod.Find<ModBuff>("Tequila").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 5, 0, 0);
		}
    }
}
