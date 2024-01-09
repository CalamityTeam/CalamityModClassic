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
	public class FirestormCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Firestorm Cannon");
			//Tooltip.SetDefault("70% chance to not consume flares\nRight click to change modes");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 11;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Flare;
			Item.shootSpeed = 5.5f;
			Item.useAmmo = 931;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 70)
	    		return false;
	    	return true;
	    }
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 27;
				Item.useAnimation = 27;
			}
			else
			{
				Item.useTime = 9;
				Item.useAnimation = 9;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				int num6 = Main.rand.Next(4, 6);
		        for (int index = 0; index < num6; ++index)
		        {
		            float SpeedX = velocity.X + (float) Main.rand.Next(-50, 51) * 0.05f;
		            float SpeedY = velocity.Y + (float) Main.rand.Next(-50, 51) * 0.05f;
		            int flare = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		            Main.projectile[flare].penetrate = 1;
		            Main.projectile[flare].timeLeft = 600;
		        }
		        return false;
			}
			else
			{
			    int num6 = Main.rand.Next(1, 3);
			    for (int index = 0; index < num6; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
			        int projectile = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.85f), knockback, player.whoAmI, 0.0f, 0.0f);
			        Main.projectile[projectile].timeLeft = 200;
			    }
			    return false;
			}
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlareGun);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(null, "VictoryShard", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlareGun);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.PlatinumBar, 10);
            recipe.AddIngredient(null, "VictoryShard", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}