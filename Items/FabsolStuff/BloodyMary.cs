using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class BloodyMary : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloody Mary");
			/* Tooltip.SetDefault(@"Boosts damage, movement speed, and melee speed by 15% and crit chance by 7% during a Blood Moon
Reduces life regen by 2 and defense by 6
Extra spicy and bloody!"); */
		}

		public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = 3;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("BloodyMary").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 10, 0, 0);
		}
    }
}
