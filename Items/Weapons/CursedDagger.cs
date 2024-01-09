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
	public class CursedDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cursed Dagger");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.damage = 34;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 16;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 34;
			Item.maxStack = 1;
			Item.value = 300000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("CursedDagger").Type;
			Item.shootSpeed = 12f;
		}
	}
}
