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
	public class Alluvion : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.damage = 140;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 58;
	        Item.useTime = 9;
	        Item.useAnimation = 18;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
	        Item.value = 10000000;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.WoodenArrowFriendly;
	        Item.shootSpeed = 17f;
	        Item.useAmmo = 40;
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
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num117 = 0.314159274f;
			int num118 = 10;
			Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
			vector7.Normalize();
			vector7 *= 20f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
			for (int num119 = 0; num119 < num118; num119++)
			{
				float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
				Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
				if (!flag11)
				{
					value9 -= vector7;
				}
				switch (Main.rand.Next(12))
				{
		    		case 1: type = Mod.Find<ModProjectile>("TorrentialArrow").Type; break;
		    		default: break;
				}
				switch (Main.rand.Next(25))
				{
		    		case 1: type = 408; break;
		    		default: break;
				}
				switch (Main.rand.Next(100))
				{
		    		case 1: type = Mod.Find<ModProjectile>("TyphoonArrow").Type; break;
		    		default: break;
				}
				int num121 = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
				Main.projectile[num121].noDropItem = true;
			}
			return false;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Monsoon");
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}