using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.FiniteUse
{
	public class ExplosiveShells : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Explosive Shotgun Shell");
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = 6;
			Item.consumable = true;
			Item.knockBack = 10f;
			Item.value = 15000;
			Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("ExplosiveShellBullet").Type;
			Item.shootSpeed = 12f;
			Item.ammo = Mod.Find<ModItem>("ExplosiveShells").Type;
		}
	}
}