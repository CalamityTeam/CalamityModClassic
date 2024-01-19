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
	public class NullificationRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Nullification Pistol");
			//Tooltip.SetDefault("Is it nullable or not?  Let's find out!\nFires a fast null bullet that distorts NPC stats\nUses your life as ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 135;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 64;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
	        Item.value = 1250000;
	        Item.rare = ItemRarityID.Cyan;
	        Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point2/Sounds/Item/PlasmaBlast");
	        Item.autoReuse = true;
	        Item.shootSpeed = 25f;
	        Item.shoot = Mod.Find<ModProjectile>("NullShot").Type;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	player.statLife -= 5;
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("NullShot").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}