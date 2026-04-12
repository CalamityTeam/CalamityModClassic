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
	public class BlackAnurian : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Black Anurian");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 40;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 58;
	        Item.height = 38;
	        Item.useTime = 14;
	        Item.useAnimation = 14;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.75f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item111;
	        Item.autoReuse = true;
	        Item.shootSpeed = 8f;
	        Item.shoot = Mod.Find<ModProjectile>("BlackAnurian").Type;
	    }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        int num6 = 2;
	        for (int index = 0; index < num6; ++index)
	        {
                float SpeedX = velocity.X + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("BlackAnurianPlankton").Type, (int)((double)damage * 0.5), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * 0.5), knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
		}
	}
}