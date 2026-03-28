using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AquaticScourge
{
	public class Downpour : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Downpour");
			// Tooltip.SetDefault("Fires a spray of water that drips extra trails of water");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 43;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 42;
	        Item.height = 42;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item13;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("WaterStream").Type;
	        Item.shootSpeed = 14f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	}
}