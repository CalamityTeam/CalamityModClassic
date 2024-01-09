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
	public class StormSpray : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Storm Spray");
			//Tooltip.SetDefault("Fires a spray of water that drips extra trails of water");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 15;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 5;
	        Item.width = 42;
	        Item.height = 42;
	        Item.useTime = 23;
	        Item.useAnimation = 23;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5;
	        Item.value = 50000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item13;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("WaterStream").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	}
}