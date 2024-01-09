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
	public class Dualpoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dualpoon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 49;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 56;
	        Item.height = 28;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 6.5f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item10;
	        Item.autoReuse = true;
	        Item.shootSpeed = 20f;
	        Item.shoot = Mod.Find<ModProjectile>("Triploon").Type;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num117 = 0.314159274f;
			int num118 = 2;
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
			}
			return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Harpoon, 2);
	        recipe.AddIngredient(ItemID.SoulofMight, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}