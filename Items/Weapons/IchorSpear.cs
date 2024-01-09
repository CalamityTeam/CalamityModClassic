using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class IchorSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ichor Spear");
		}

		public override void SetDefaults()
		{
			Item.width = 52;
			Item.damage = 40;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 52;
			Item.value = 300000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("IchorSpear").Type;
			Item.shootSpeed = 20f;
		}
	}
}
