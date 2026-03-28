using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Fireball : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fireball");
			/* Tooltip.SetDefault(@"Boosts all fire-based weapon damage by 10%
Cursed flame, shadowflame, god slayer inferno, brimstone flame, and frostburn weapons will not receive this benefit
The weapon must be more fire-related than anything else
Reduces life regen by 1
A great-tasting cinnamon whiskey"); */
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
            Item.buffType = Mod.Find<ModBuff>("Fireball").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 6, 60, 0);
		}
    }
}
