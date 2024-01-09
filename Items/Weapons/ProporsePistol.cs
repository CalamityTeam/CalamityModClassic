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
	public class ProporsePistol : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Proporse Pistol");
			//Tooltip.SetDefault("Fires a blue energy blast that bounces on tile hits");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 45;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 36;
	        Item.height = 20;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
	        Item.value = 100000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 20f;
	        Item.shoot = Mod.Find<ModProjectile>("ProBolt").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ProBolt").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}