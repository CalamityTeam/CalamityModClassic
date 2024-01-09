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
	public class Monsoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Monsoon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 63;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 30;
	        Item.height = 62;
	        Item.useTime = 21;
	        Item.useAnimation = 21;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 1000000;
	        Item.rare = ItemRarityID.Cyan;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.WoodenArrowFriendly;
	        Item.shootSpeed = 10f;
	        Item.useAmmo = 40;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num117 = 0.314159274f;
			int num118 = 5;
			Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
			vector7.Normalize();
			vector7 *= 40f;
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
		    		case 1: type = 408; break;
		    		default: break;
				}
				switch (Main.rand.Next(25))
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
	        recipe.AddIngredient(ItemID.FragmentVortex, 20);
	        recipe.AddIngredient(ItemID.Tsunami);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}