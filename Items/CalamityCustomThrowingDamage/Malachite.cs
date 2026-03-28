using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class Malachite : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Malachite");
			/* Tooltip.SetDefault("Legendary Drop\n" +
				"Throws a stream of kunai that stick to enemies and explode\n" +
				"Right click to throw a single kunai that pierces, after piercing an enemy it emits a massive explosion on the next enemy hit\n" +
                "Revengeance drop"); */
		}

		public override void SafeSetDefaults()
		{
			Item.width = 26;
			Item.damage = 58;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 1;
			Item.knockBack = 1.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 58;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.shoot = Mod.Find<ModProjectile>("Malachite").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 17;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 10;
	    		Item.useAnimation = 10;
	        	Item.UseSound = SoundID.Item109;
			}
			else
			{
				Item.useTime = 5;
	    		Item.useAnimation = 5;
	        	Item.UseSound = SoundID.Item1;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("MalachiteBolt").Type, (int)((double)damage * 2.0), knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	        	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Malachite").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	}
}
