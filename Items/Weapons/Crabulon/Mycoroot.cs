using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Crabulon
{
	public class Mycoroot : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mycoroot");
			//Tooltip.SetDefault("Fires a stream of short-range fungal roots");
		}

		public override void SetDefaults()
		{
			Item.width = 32;  //The width of the .png file in pixels divided by 2.
			Item.damage = 10;  //Keep this reasonable please.
			Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1.75f;  //Ranges from 1 to 9.
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
			Item.height = 32;  //The height of the .png file in pixels divided by 2.
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Green;
			Item.value = 40000;  //Value is calculated in copper coins.
			Item.shoot = Mod.Find<ModProjectile>("Mycoroot").Type;
			Item.shootSpeed = 20f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
		    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	}
}
