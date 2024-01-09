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
	public class VividClarity : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 450;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 50;
	        Item.width = 82;
	        Item.height = 82;
	        Item.useTime = 35;
	        Item.useAnimation = 35;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
	        Item.value = 100000000;
	        Item.UseSound = SoundID.Item60;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("VividClarity").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			float num72 = Item.shootSpeed;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X + vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y + vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + vector2.Y;
			}
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			float num81 = num80;
			if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
			{
				num78 = (float)player.direction;
				num79 = 0f;
				num80 = num72;
			}
			else
			{
				num80 = num72 / num80;
			}
	    	num78 *= num80;
			num79 *= num80;
	    	int num130 = 3;
			for (int num131 = 0; num131 < num130; num131++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
				vector2.Y -= (float)(100 * num131);
				num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (num79 < 0f)
				{
					num79 *= -1f;
				}
				if (num79 < 20f)
				{
					num79 = 20f;
				}
				num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
				num80 = num72 / num80;
				num78 *= num80;
				num79 *= num80;
				float speedX4 = num78 + (float)Main.rand.Next(-40, 41) * 0.02f;
				float speedY4 = num79 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("VividClarity").Type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ElementalRay");
	        recipe.AddIngredient(null, "ArchAmaryllis");
	        recipe.AddIngredient(null, "AsteroidStaff");
	        recipe.AddIngredient(null, "HellwingStaff");
	        recipe.AddIngredient(null, "PhantasmalFury");
	        recipe.AddIngredient(null, "ShadowboltStaff");
	        recipe.AddIngredient(null, "VenusianTrident");
	        recipe.AddIngredient(null, "NightmareFuel", 10);
        	recipe.AddIngredient(null, "EndothermicEnergy", 10);
	        recipe.AddIngredient(null, "CosmiliteBar", 10);
	        recipe.AddIngredient(null, "Phantoplasm", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}