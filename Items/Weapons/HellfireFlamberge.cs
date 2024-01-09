using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class HellfireFlamberge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/HellfireFlamberge");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Hellfire Flamberge");
		Item.width = 84;  //The width of the .png file in pixels divided by 2.
		Item.damage = 70;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Magic;
		Item.useAnimation = 32;
		Item.useStyle = 1;
		Item.useTime = 32;
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 94;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 415000;  //Value is calculated in copper coins.
		Item.rare = 7;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ChaosFlameSmall").Type;
		Item.shootSpeed = 16f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
            float speedX = velocity.X;
            float speedY = velocity.Y;
    	float SpeedA = speedX;
   		float SpeedB = speedY;
        int num6 = Main.rand.Next(3, 5);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = speedX;
            float num8 = speedY;
            float SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
    		switch (Main.rand.Next(6))
			{
    			case 1: type = Mod.Find<ModProjectile>("ChaosFlameSmall").Type; break;
    			case 2: type = Mod.Find<ModProjectile>("ChaosFlameMedium").Type; break;
    			case 3: type = Mod.Find<ModProjectile>("ChaosFlameLarge").Type; break;
    			default: break;
			}
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 15);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 174);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}
}}
