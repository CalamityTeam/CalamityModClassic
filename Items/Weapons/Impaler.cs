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
	public class Impaler : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Impaler");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 85;
	        Item.DamageType = DamageClass.Ranged;
	        Item.crit += 14;
	        Item.width = 40;
	        Item.height = 26;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
	        Item.value = 1050000;
	        Item.rare = ItemRarityID.Cyan;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FlamingStake").Type;
	        Item.shootSpeed = 10f;
	        Item.useAmmo = 1836;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, -10);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        float SpeedX = velocity.X + (float) Main.rand.Next(-5, 6) * 0.05f;
	        float SpeedY = velocity.Y + (float) Main.rand.Next(-5, 6) * 0.05f;
	        if (Main.rand.NextBool(3))
	        {
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("ExplodingStake").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        else
	        {
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("FlamingStake").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CoreofChaos", 5);
	        recipe.AddIngredient(ItemID.StakeLauncher);
	        recipe.AddIngredient(ItemID.ExplosivePowder, 100);
	        recipe.AddIngredient(ItemID.LivingFireBlock, 75);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}