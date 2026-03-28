using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.HiveMind
{
	public class ShaderainStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shaderain Staff");
			// Tooltip.SetDefault("Fires a shade storm cloud");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 17;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 34;
	        Item.height = 34;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item66;
	        Item.shoot = Mod.Find<ModProjectile>("ShadeNimbus").Type;
	        Item.shootSpeed = 16f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float num72 = Item.shootSpeed;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
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
			int num154 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("ShadeNimbusCloud").Type, damage, knockback, player.whoAmI, 0f, 0f);
			Main.projectile[num154].ai[0] = (float)Main.mouseX + Main.screenPosition.X;
			Main.projectile[num154].ai[1] = (float)Main.mouseY + Main.screenPosition.Y;
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RottenChunk, 2);
	        recipe.AddIngredient(ItemID.DemoniteBar, 3);
	        recipe.AddIngredient(null, "TrueShadowScale", 12);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}