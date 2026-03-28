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
	public class AlphaRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Alpha Ray");
			/* Tooltip.SetDefault("Disintegrates everything\n" +
				"Right click to change modes"); */
		}


	    public override void SetDefaults()
	    {
	        Item.damage = 240;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 5;
	        Item.width = 84;
	        Item.height = 74;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 6f;
	        Item.shoot = Mod.Find<ModProjectile>("ParticleBeamofDoom").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BigBeamofDeath").Type, (int)((double)damage * 1.7), knockback, player.whoAmI, 0.0f, 0.0f);
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
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage * 0.8), knockback, player.whoAmI, 0.0f, 0.0f);
					int laser = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X * 2f, velocity.Y * 2f, 440, (int)((double)damage * 0.4), knockback, player.whoAmI, 0.0f, 0.0f);
					Main.projectile[laser].timeLeft = 120;
		        	Main.projectile[laser].tileCollide = false;
				}
				return false;
	    	}
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(null, "DarksunFragment", 15);
            recipe.AddIngredient(null, "Wingman", 2);
	        recipe.AddIngredient(null, "Genisis");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}