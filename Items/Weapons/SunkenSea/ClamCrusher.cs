using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
	public class ClamCrusher : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Clam Crusher");
			/* Tooltip.SetDefault("Launches a huge clam that stuns enemies for a short amount of time\n" +
							   "Starts being affected by gravity and does much more damage after being airborne for a while"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 140;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 40;
	        Item.height = 60;
	        Item.useTime = 50;
	        Item.useAnimation = 50;
	        Item.useStyle = 5;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
	        Item.knockBack = 10f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.rare = 5;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.channel = true;
	        Item.shoot = Mod.Find<ModProjectile>("ClamCrusherFlail").Type;
	        Item.shootSpeed = 18f;
	    }
	}
}