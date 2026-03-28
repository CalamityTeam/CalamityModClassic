using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons.AquaticScourge
{
	public class Barinautical : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Barinautical");
			// Tooltip.SetDefault("Shoots electric bolt arrows that explode");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 30;
	        Item.height = 42;
	        Item.useTime = 2;
            Item.reuseDelay = 15;
	        Item.useAnimation = 6;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 15f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BoltArrow").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}