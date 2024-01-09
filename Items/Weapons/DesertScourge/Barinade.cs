using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.DesertScourge
{
	public class Barinade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Barinade");
			//Tooltip.SetDefault("Shoots electric bolt arrows that explode");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 12;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 30;
	        Item.height = 44;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
	        Item.value = 35000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 18f;
	        Item.useAmmo = 40;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BoltArrow").Type, (int)((double)damage * 0.5f), knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}