using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.FiniteUse
{
	public class MagnumRounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magnum Rounds");
		}

		public override void SetDefaults()
		{
			Item.damage = 80;
            Item.crit += 4;
            Item.width = 18;
			Item.height = 18;
			Item.maxStack = 12;
			Item.consumable = true;
			Item.knockBack = 8f;
			Item.value = 10000;
			Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("MagnumRound").Type;
			Item.shootSpeed = 12f;
			Item.ammo = Mod.Find<ModItem>("MagnumRounds").Type;
		}
	}
}