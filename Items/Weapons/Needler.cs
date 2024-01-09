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
	public class Needler : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Needler");
			//Tooltip.SetDefault("Fires needles that stick to enemies and explode");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 35;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 26;
	        Item.useTime = 18;
	        Item.useAnimation = 18;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
	        Item.value = 250000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item108;
	        Item.autoReuse = true;
	        Item.shootSpeed = 9f;
	        Item.shoot = Mod.Find<ModProjectile>("Needler").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Needler").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}