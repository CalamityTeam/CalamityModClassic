using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class YharimsCrystal : ModItem
	{

	    public override void SetDefaults()
	    {
	        Item.damage = 250;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 125;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
	        Item.value = 100000000;
	        Item.shoot = Mod.Find<ModProjectile>("YharimsCrystal").Type;
	        Item.shootSpeed = 30f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, Main.DiscoG, 53);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	bool playerName = 
	    		player.name == "Fabsol" ||
	    		player.name == "Ziggums" || 
	    		player.name == "Poly" || 
	    		player.name == "Zach" || 
	    		player.name == "Grox" || 
	    		player.name == "Jenosis" || 
	    		player.name == "DM DOKURO" || 
	    		player.name == "Uncle Danny" || 
	    		player.name == "Phoenix" || 
	    		player.name == "Vlad" || 
	    		player.name == "Khaelis" || 
	    		player.name == "Purple Necromancer" || 
	    		player.name == "Spoopyro" || 
	    		player.name == "Svante" || 
	    		player.name == "Puff" || 
	    		player.name == "Echo" || 
	    		player.name == "Testdude";
	    	bool yharon = CalamityWorld1Point2.downedYharon;
	    	if (playerName || yharon)
	    	{
	    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("YharimsCrystal").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, 29, 0, 0f, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	}
}