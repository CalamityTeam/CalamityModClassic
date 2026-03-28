using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class CinnamonRoll : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cinnamon Roll");
			/* Tooltip.SetDefault(@"Boosts mana regeneration rate and all fire-based weapon damage by 15%
Cursed flame, shadowflame, god slayer inferno, brimstone flame, and frostburn weapons will not receive this benefit
The weapon must be more fire-related than anything else
Reduces defense by 12
A great-tasting cinnamon whiskey with a touch of cream soda"); */
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
            Item.buffType = Mod.Find<ModBuff>("CinnamonRoll").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 16, 60, 0);
		}
    }
}
