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
	public class SpectralstormCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spectralstorm Cannon");
			//Tooltip.SetDefault("70% chance to not consume flares\nFires a storm of ectoplasm and flares");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 62;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 66;
			Item.height = 26;
			Item.useTime = 3;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.value = 900000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Flare;
			Item.shootSpeed = 9.5f;
			Item.useAmmo = 931;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 70)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    int num6 = Main.rand.Next(1, 2);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
		        int projectile = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		        Main.projectile[projectile].timeLeft = 200;
		    }
		    int projectile2 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 297, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    Main.projectile[projectile2].DamageType = DamageClass.Ranged;
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FirestormCannon");
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}