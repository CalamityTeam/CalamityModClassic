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
	public class BarracudaGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Barracuda Gun");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 72;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 54;
	        Item.height = 28;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 1.05f;
	        Item.value = 900000;
	        Item.rare = ItemRarityID.Cyan;
	        Item.UseSound = SoundID.Item10;
	        Item.autoReuse = true;
	        Item.shootSpeed = 15f;
	        Item.shoot = Mod.Find<ModProjectile>("MechanicalBarracuda").Type;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			float SpeedA = velocity.X;
	   		float SpeedB = velocity.Y;
	        int num6 = Main.rand.Next(2, 3);
	        for (int index = 0; index < num6; ++index)
	        {
	      	 	float num7 = velocity.X;
	            float num8 = velocity.Y;
	            float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.PiranhaGun);
	        recipe.AddIngredient(null, "CoreofCalamity", 2);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}