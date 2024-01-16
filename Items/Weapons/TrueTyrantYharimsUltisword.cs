using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TrueTyrantYharimsUltisword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/TrueTyrantYharimsUltisword");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("True Tyrant's Ultisword");
		Item.width = 84;  //The width of the .png file in pixels divided by 2.
		Item.damage = 152;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 26;
		Item.useStyle = 1;
		Item.useTime = 26;
		Item.useTurn = true;
		Item.knockBack = 6.50f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 84;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Contains the essence of a forgotten age\n50% chance to give the player the tyrant's fury buff on enemy hits\nThis buff increases melee damage, speed, and crit chance by 30%\nLaunches blazing phantom, hyper, and sunlight blades");
		Item.value = 5250000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type;
		Item.shootSpeed = 12f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(3))
		{
    		case 0: type = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type; break;
    		case 1: type = Mod.Find<ModProjectile>("HyperBlade").Type; break;
    		case 2: type = Mod.Find<ModProjectile>("SunlightBlade").Type; break;
    		default: break;
		}
       	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, Main.myPlayer);
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TyrantYharimsUltisword");
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(null, "UeliaceBar", 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 106);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (Main.rand.Next(2) == 0)
    	{
    		player.AddBuff(Mod.Find<ModBuff>("TyrantsFury").Type, 240);
    	}
		target.AddBuff(BuffID.OnFire, 300);
		target.AddBuff(BuffID.Venom, 240);
		target.AddBuff(BuffID.CursedInferno, 180);
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
		target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
	}
}}
