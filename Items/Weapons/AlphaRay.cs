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
	public class AlphaRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Alpha Ray");
			//Tooltip.SetDefault("Disintegrates everything\nRight click to change modes");
		}


	    public override void SetDefaults()
	    {
	        Item.damage = 125;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 78;
	        Item.height = 70;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 1.5f;
	        Item.value = 10000000;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 6f;
	        Item.shoot = Mod.Find<ModProjectile>("ParticleBeamofDoom").Type;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BigBeamofDeath").Type, (int)((double)damage * 3f), knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
		    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		    	float num117 = 0.314159274f;
				int num118 = 3;
				Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
				vector7.Normalize();
				vector7 *= 80f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
				for (int num119 = 0; num119 < num118; num119++)
				{
					float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
					Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
					if (!flag11)
					{
						value9 -= vector7;
					}
					Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
					int laser = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, 440, (int)((double)damage * 0.35f), knockback, player.whoAmI, 0.0f, 0.0f);
					Main.projectile[laser].timeLeft = 120;
		        	Main.projectile[laser].velocity.X *= 2f;
		        	Main.projectile[laser].velocity.Y *= 2f;
		        	Main.projectile[laser].tileCollide = false;
				}
				return false;
	    	}
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(null, "CosmiliteBar", 8);
	        recipe.AddIngredient(null, "Wingman", 2);
	        recipe.AddIngredient(null, "Genisis");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}