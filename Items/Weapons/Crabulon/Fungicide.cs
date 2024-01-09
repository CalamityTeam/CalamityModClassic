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
	public class Fungicide : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fungicide");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 14;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 40;
	        Item.height = 26;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true; //so the item's animation doesn't do damage
	        Item.knockBack = 2.5f;
	        Item.value = 40000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item61;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FungiOrb").Type; //idk why but all the guns in the vanilla source have this
	        Item.shootSpeed = 14f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("FungiOrb").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}