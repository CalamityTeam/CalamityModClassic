using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Crabulon
{
	public class Fungicide : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungicide");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 11;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 40;
	        Item.height = 26;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item61;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FungiOrb").Type;
	        Item.shootSpeed = 14f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("FungiOrb").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}