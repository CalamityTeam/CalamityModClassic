using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class StellarKnife : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stellar Knife");
			/* Tooltip.SetDefault("Throws knives that stop middair and then home into enemies\n" +
							   "Za Warudo"); */
		}

		public override void SafeSetDefaults()
		{
			Item.width = 32;
			Item.height = 34;
			Item.damage = 60;
			Item.crit += 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 8;
			Item.useStyle = 1;
			Item.useTime = 8;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("StellarKnife").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
	}
}
