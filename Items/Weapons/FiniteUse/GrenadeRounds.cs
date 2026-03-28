using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.FiniteUse
{
	public class GrenadeRounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grenade Shells");
		}

		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = 9;
			Item.consumable = true;
			Item.knockBack = 10f;
			Item.value = 15000;
			Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("GrenadeRound").Type;
			Item.shootSpeed = 12f;
			Item.ammo = Mod.Find<ModItem>("GrenadeRounds").Type;
		}
	}
}