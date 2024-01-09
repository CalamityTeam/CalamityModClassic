using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Bumblebirb
{
	public class GoldenEagle : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Golden Eagle");
			//Tooltip.SetDefault("Fires 5 bullets at once");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 58;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 46;
			Item.height = 30;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3f;
			Item.value = 1000000;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 20f;
			Item.useAmmo = 97;
		}
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    float SpeedX = velocity.X + 5f * 0.05f;
	        float SpeedY = velocity.Y + 5f * 0.05f;
	        float SpeedX2 = velocity.X - 5f * 0.05f;
	        float SpeedY2 = velocity.Y - 5f * 0.05f;
	        float SpeedX3 = velocity.X + 0f * 0.05f;
	        float SpeedY3 = velocity.Y + 0f * 0.05f;
	        float SpeedX4 = velocity.X - 10f * 0.05f;
	        float SpeedY4 = velocity.Y - 10f * 0.05f;
	        float SpeedX5 = velocity.X + 10f * 0.05f;
	        float SpeedY5 = velocity.Y + 10f * 0.05f;
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX4, SpeedY4, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX5, SpeedY5, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}