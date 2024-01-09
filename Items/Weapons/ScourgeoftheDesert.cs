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
	public class ScourgeoftheDesert : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Scourge of the Desert");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 18;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.value = 15000;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("ScourgeoftheDesert").Type;
			Item.shootSpeed = 12f;
		}
	}
}
