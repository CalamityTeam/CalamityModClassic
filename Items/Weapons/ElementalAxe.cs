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

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class ElementalAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Elemental Axe");
			//Tooltip.SetDefault("Summons an elemental axe to fight for you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 225;
	        Item.DamageType = DamageClass.Summon;
	        Item.mana = 10;
	        Item.width = 36;
	        Item.height = 36;
	        Item.useTime = 36;
	        Item.useAnimation = 36;
	        Item.useStyle = ItemUseStyleID.Swing;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
	        Item.buffType = Mod.Find<ModBuff>("ElementalAxe").Type;
	        Item.buffTime = 3600;
	        Item.value = 10000000;
	        Item.rare = ItemRarityID.Red;
	        Item.UseSound = SoundID.Item44;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ElementalAxeG").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "BarofLife", 5);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(null, "InfernaCutter");
	        recipe.AddTile(TileID.LunarCraftingStation);
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
			type = Main.rand.Next(7);
			if (type == 0)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeR").Type;
			}
			else if (type == 1)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeO").Type;
			}
			else if (type == 2)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeY").Type;
			}
			else if (type == 3)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeG").Type;
			}
			else if (type == 4)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeB").Type;
			}
			else if (type == 5)
			{
				type = Mod.Find<ModProjectile>("ElementalAxeI").Type;
			}
			else
			{
				type = Mod.Find<ModProjectile>("ElementalAxeV").Type;
			}
			Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, type, damage, knockback, player.whoAmI, 0f, 0f);
			return false;
	    }
	}
}