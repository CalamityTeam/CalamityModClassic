using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class Terratomere : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/Terratomere");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Terratomere");
		Item.width = 56;  //The width of the .png file in pixels divided by 2.
		Item.damage = 155;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.useTime = 20;
		Item.knockBack = 7;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		//Tooltip.SetDefault("Linked to the essence of Terraria");
		Item.value = 2400000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TerratomereProjectile").Type;
		Item.shootSpeed = 20f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float speedX = velocity.X;
			float speedY = velocity.Y;
			float SpeedA = speedX;
   		float SpeedB = speedY;
        int num6 = Main.rand.Next(4, 6);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = speedX;
            float num8 = speedY;
            float SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.5), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 5);
		recipe.AddIngredient(null, "VerstaltiteBar", 5);
		recipe.AddIngredient(null, "DraedonBar", 5);
		recipe.AddIngredient(null, "UeliaceBar", 5);
		recipe.AddIngredient(null, "MeldiateBar", 5);
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(null, "XerocsGreatsword");
		recipe.AddIngredient(null, "Mariana");
		recipe.AddIngredient(null, "Hellkite");
		recipe.AddIngredient(null, "TemporalFloeSword");
		recipe.AddIngredient(ItemID.TerraBlade);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.CursedInferno, 680);
		target.AddBuff(BuffID.Frostburn, 620);
		target.AddBuff(BuffID.Frozen, 160);
		target.AddBuff(BuffID.Chilled, 1200);
		target.AddBuff(BuffID.OnFire, 600);
	}
}}
