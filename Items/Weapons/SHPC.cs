using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class SHPC : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SHPC");
			/* Tooltip.SetDefault("Legendary Drop\n" +
				"Fires plasma orbs that linger and emit massive explosions\n" +
				"Right click to fire a powerful energy beam\n" +
                "Revengeance drop"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 18;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 96;
	        Item.height = 34;
	        Item.useTime = 50;
	        Item.useAnimation = 50;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
            Item.rare = 5;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.UseSound = SoundID.Item92;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("SHPB").Type;
	        Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 17;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-25, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 7;
	    		Item.useAnimation = 7;
	    		Item.mana = 6;
	        	Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
			}
			else
			{
				Item.useTime = 50;
	        	Item.useAnimation = 50;
	        	Item.mana = 20;
	        	Item.UseSound = SoundID.Item92;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	        	for (int shootAmt = 0; shootAmt < 3; shootAmt++)
	        	{
	        		float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
		    		float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	        		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SHPL").Type, damage, (Item.knockBack * 0.5f), player.whoAmI, 0.0f, 0.0f);
	        	}
	    		return false;
	    	}
	    	else
	    	{
	        	for (int shootAmt = 0; shootAmt < 3; shootAmt++)
	        	{
	        		float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
		    		float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	        		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SHPB").Type, (int)((double)damage * 1.1f), knockback, player.whoAmI, 0.0f, 0.0f);
	        	}
	    		return false;
	    	}
		}
	}
}