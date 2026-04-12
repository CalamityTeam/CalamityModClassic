using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Ravager
{
	public class RealmRavager : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Realm Ravager");
			// Tooltip.SetDefault("Shoots a burst of explosive bullets");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 50;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 76;
	        Item.height = 32;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item38;
	        Item.autoReuse = true;
	        Item.shootSpeed = 30f;
	        Item.shoot = Mod.Find<ModProjectile>("RealmRavagerBullet").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        for (int index = 0; index < 5; ++index)
			{
				float SpeedX = velocity.X + (float)Main.rand.Next(-75, 76) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-75, 76) * 0.05f;
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("RealmRavagerBullet").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
	        return false;
	    }
	}
}