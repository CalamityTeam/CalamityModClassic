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
	public class YharimsCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yharim's Crystal");
			/* Tooltip.SetDefault("Fires a beam of complete destruction\n" +
	        	"Only those that are worthy can use this item before Yharon is defeated"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 240;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 100;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = 5;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("YharimsCrystal").Type;
	        Item.shootSpeed = 30f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 17;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	bool playerName = 
	    		player.name == "Fabsol" ||
	    		player.name == "Ziggums" || 
	    		player.name == "Poly" || 
	    		player.name == "Zach" || 
	    		player.name == "Grox the Great" || 
	    		player.name == "Jenosis" || 
	    		player.name == "DM DOKURO" || 
	    		player.name == "Uncle Danny" || 
	    		player.name == "Phoenix" || 
	    		player.name == "MineCat" || 
	    		player.name == "Khaelis" || 
	    		player.name == "Purple Necromancer" || 
	    		player.name == "gamagamer64" || 
	    		player.name == "Svante" || 
	    		player.name == "Puff" || 
	    		player.name == "Leviathan" || 
	    		player.name == "Testdude";
	    	bool yharon = CalamityWorldPreTrailer.downedYharon;
	    	if (playerName || yharon)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("YharimsCrystal").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, 0f, 0f, 29, 0, 0f, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	}
}