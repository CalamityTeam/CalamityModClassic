using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class PlanetaryAnnihilation : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Planetary Annihilation");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 75;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 102;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item75;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("TerraBall").Type;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float num72 = Main.rand.Next(19, 35);
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
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= 100f;
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
			float speedX4 = num78 + (float)Main.rand.Next(-240, 241) * 0.02f;
			float speedY5 = num79 + (float)Main.rand.Next(-240, 241) * 0.02f;
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.3f), Mod.Find<ModProjectile>("TerraBallR").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(2));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.2f), Mod.Find<ModProjectile>("TerraBallO").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(3));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.1f), Mod.Find<ModProjectile>("TerraBallY").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(4));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1f), Mod.Find<ModProjectile>("TerraBallG").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(5));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.9f), Mod.Find<ModProjectile>("TerraBallB").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(6));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.8f), Mod.Find<ModProjectile>("TerraBallI").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(7));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.7f), Mod.Find<ModProjectile>("TerraBallV").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(8));
            }
            else
            {
                int num121 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.3f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num121].noDropItem = true;
                int num122 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.2f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num122].noDropItem = true;
                int num123 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1.1f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num123].noDropItem = true;
                int num124 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 1f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num124].noDropItem = true;
                int num125 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.9f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num125].noDropItem = true;
                int num126 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.8f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num126].noDropItem = true;
                int num127 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, (speedY5 * 0.7f), type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[num127].noDropItem = true;
            }
            return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(null, "CosmicBolter");
	        recipe.AddIngredient(ItemID.DaedalusStormbow);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}