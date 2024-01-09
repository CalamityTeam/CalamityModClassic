using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Perforators
{
	public class BloodClotStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Blood Clot Staff");
			//Tooltip.SetDefault("Summons a blood clot to fight for you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 12;
	        Item.mana = 10;
	        Item.width = 58;
	        Item.height = 64;
	        Item.useTime = 36;
	        Item.useAnimation = 36;
	        Item.useStyle = ItemUseStyleID.Swing;
	        Item.noMelee = true;
	        Item.knockBack = 2.25f;
	        Item.value = 55000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item44;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BloodClotMinion").Type;
	        Item.shootSpeed = 10f;
	        Item.DamageType = DamageClass.Summon;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Vertebrae, 4);
	        recipe.AddIngredient(ItemID.CrimtaneBar, 5);
	        recipe.AddIngredient(null, "BloodSample", 10);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
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
	    	num78 = 0f;
			num79 = 0f;
			vector2.X = (float)Main.mouseX + Main.screenPosition.X;
			vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
			Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("BloodClotMinion").Type, damage, knockback, player.whoAmI, 0f, 0f);
			return false;
	    }
	}
}