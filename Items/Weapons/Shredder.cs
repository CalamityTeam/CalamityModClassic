using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons
{
	public class Shredder : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture = "CalamityModClassic1Point0/Items/Weapons/Shredder");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //DisplayName.SetDefault("Shredder");
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			//Tooltip.SetDefault("Only the first shot consumes ammo");
			Item.useTime = 3;
			Item.reuseDelay = 14;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.5f;
			Item.value = 1000000;
			Item.rare = 10;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6.5f;
			Item.useAmmo = ProjectileID.Bullet;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float speedX = velocity.X;
			float speedY = velocity.Y;
			float SpeedX = speedX;
	    float SpeedY = speedY;
	    if (Item.useAnimation < 5)
	    {
	        float num6 = SpeedX + (float) Main.rand.Next(-40, 41) * 0.01f;
	        float num7 = SpeedY + (float) Main.rand.Next(-40, 41) * 0.01f;
	        SpeedX = num6 * 1.1f;
	        SpeedY = num7 * 1.1f;
	        int num8 = Main.rand.Next(5, 7);
    		for (int index = 0; index < num6; ++index)
    		{
        		float num9 = speedX;
   			    float num10 = speedY;
        		SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
        		SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
        		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    		}
	     }
	     else if (Item.useAnimation < 10)
	     {
	         float num6 = SpeedX + (float) Main.rand.Next(-20, 21) * 0.01f;
	         float num7 = SpeedY + (float) Main.rand.Next(-20, 21) * 0.01f;
	         SpeedX = num6 * 1.05f;
	         SpeedY = num7 * 1.05f;
	         int num8 = Main.rand.Next(4, 6);
    		 for (int index = 0; index < num6; ++index)
    		 {
        		 float num9 = speedX;
        		 float num10 = speedY;
        		 SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
        		 SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
        		 Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    		 }
	      }
	     return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BarofLife", 5);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(null, "CoreofCalamity");
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(null, "ParticleAccelerator");
            recipe.Register();
		}
	}
}