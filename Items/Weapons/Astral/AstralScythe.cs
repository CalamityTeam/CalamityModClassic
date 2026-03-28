using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Astral
{
	public class AstralScythe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Scythe");
			// Tooltip.SetDefault("Shoots a scythe ring that accelerates over time");
		}

		public override void SetDefaults()
		{
			Item.width = 56;
			Item.height = 60;
			Item.damage = 95;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useTurn = true;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.useTime = 20;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.shoot = Mod.Find<ModProjectile>("AstralScytheProjectile").Type;
			Item.shootSpeed = 5f;
		}
	}
}
