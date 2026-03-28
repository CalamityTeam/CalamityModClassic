using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class StarBeamRye : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Star Beam Rye");
			/* Tooltip.SetDefault(@"Boosts max mana by 50, magic damage by 8%,
and reduces mana usage by 10%
Reduces defense by 6 and life regen by 1
Made from some stuff I found near the Astral Meteor crash site, don't worry it's safe, trust me"); */
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
            Item.buffType = Mod.Find<ModBuff>("StarBeamRye").Type;
            Item.buffTime = 18000; //5 minutes
            Item.value = Item.buyPrice(0, 13, 30, 0);
		}
    }
}
