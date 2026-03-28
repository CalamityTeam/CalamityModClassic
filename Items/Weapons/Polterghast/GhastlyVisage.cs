using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Polterghast
{
	public class GhastlyVisage : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ghastly Visage");
			// Tooltip.SetDefault("Fires homing ghast energy that explodes");
		}


	    public override void SetDefaults()
	    {
	        Item.damage = 92;
	        Item.DamageType = DamageClass.Magic;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.mana = 20;
	        Item.width = 78;
	        Item.height = 70;
	        Item.useTime = 27;
	        Item.useAnimation = 27;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 9f;
	        Item.shoot = Mod.Find<ModProjectile>("GhastlyVisage").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("GhastlyVisage").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}