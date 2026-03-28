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
	public class InfernalRift : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infernal Rift");
			// Tooltip.SetDefault("Summons infernal blades");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 40;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useAnimation = 16;
	        Item.useTime = 4;
	        Item.reuseDelay = Item.useAnimation + 6;
	        Item.crit = 25;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item9;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("InfernalBlade").Type;
	        Item.shootSpeed = 16f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SkyFracture);
	        recipe.AddIngredient(null, "EssenceofChaos", 3);
	        recipe.AddIngredient(ItemID.SoulofFright, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num72 = Item.shootSpeed;
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			float f = Main.rand.NextFloat() * 6.28318548f;
			float value12 = 20f;
			float value13 = 60f;
			Vector2 vector13 = vector2 + f.ToRotationVector2() * MathHelper.Lerp(value12, value13, Main.rand.NextFloat());
			for (int num202 = 0; num202 < 50; num202++)
			{
				vector13 = vector2 + f.ToRotationVector2() * MathHelper.Lerp(value12, value13, Main.rand.NextFloat());
				if (Collision.CanHit(vector2, 0, 0, vector13 + (vector13 - vector2).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
				{
					break;
				}
				f = Main.rand.NextFloat() * 6.28318548f;
			}
			Vector2 mouseWorld = Main.MouseWorld;
			Vector2 vector14 = mouseWorld - vector13;
			Vector2 vector15 = new Vector2(num78, num79).SafeNormalize(Vector2.UnitY) * num72;
			vector14 = vector14.SafeNormalize(vector15) * num72;
			vector14 = Vector2.Lerp(vector14, vector15, 0.25f);
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector13, vector14, type, damage, knockback, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}