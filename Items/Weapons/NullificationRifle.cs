using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class NullificationRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nullification Pistol");
			// Tooltip.SetDefault("Is it nullable or not?  Let's find out!\nFires a fast null bullet that distorts NPC stats\nUses your life as ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 135;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 64;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 25f;
	        Item.shoot = Mod.Find<ModProjectile>("NullShot").Type;
	    }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	player.statLife -= 5;
			if (Main.myPlayer == player.whoAmI)
			{
				player.HealEffect(-5, true);
			}
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("NullShot").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}