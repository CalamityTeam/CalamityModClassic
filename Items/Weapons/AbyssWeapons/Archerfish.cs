using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class Archerfish : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Archerfish");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 23;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 78;
	        Item.height = 36;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Archerfish").Type;
	        Item.shootSpeed = 11f;
	        Item.useAmmo = 97;
	    }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Archerfish").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	}
}