using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class GalacticaBlade : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/GalacticaBlade");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Galactica Blade");
		Item.width = 100;  //The width of the .png file in pixels divided by 2.
		Item.damage = 148;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 17;
		Item.useStyle = 1;
		Item.useTime = 17;
		Item.knockBack = 6;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 100;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		//Tooltip.SetDefault("Forged with the fury of nuclear chaos");
		Item.value = 2250000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("GalacticaComet").Type;
		Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.InfluxWaver, 1);
		recipe.AddIngredient(ItemID.Starfury, 1);
		recipe.AddIngredient(ItemID.FragmentStardust, 10);
		recipe.AddIngredient(ItemID.Ectoplasm, 20);
		recipe.AddIngredient(ItemID.SoulofMight, 50);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 30);
		recipe.AddIngredient(ItemID.DarkShard, 3);
		recipe.AddIngredient(ItemID.LightShard, 3);
		recipe.AddTile(null, "ParticleAccelerator");
		recipe.Register();
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
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
		}
		return false;
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, Mod.Find<ModDust>("GBSparkle").Type);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 1200);
		target.AddBuff(BuffID.CursedInferno, 600);
	}
}}
