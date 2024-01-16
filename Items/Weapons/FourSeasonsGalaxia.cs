using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FourSeasonsGalaxia : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/FourSeasonsGalaxia");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Galaxia");
		Item.width = 64;  //The width of the .png file in pixels divided by 2.
		Item.damage = 260;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 15;
		Item.useTime = 15;  //Ranges from 1 to 55.
		////Tooltip.SetDefault("Fires different homing projectiles based on what biome you're in\nUpon hitting an enemy you are granted a buff based on what biome you're in\nProjectiles also change based on moon events");
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 9;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 64;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Galaxia").Type;
		Item.shootSpeed = 24f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "OmegaBiomeBlade");
		recipe.AddIngredient(null, "CosmiliteBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 0);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	bool jungle = player.ZoneJungle;
        bool snow = player.ZoneSnow;
       	bool beach = player.ZoneBeach;
       	bool corrupt = player.ZoneCorrupt;
       	bool crimson = player.ZoneCrimson;
       	bool dungeon = player.ZoneDungeon;
       	bool desert = player.ZoneDesert;
       	bool glow = player.ZoneGlowshroom;
       	bool hell = player.ZoneUnderworldHeight;
       	bool holy = player.ZoneHallow;
       	bool nebula = player.ZoneTowerNebula;
       	bool stardust = player.ZoneTowerStardust;
       	bool solar = player.ZoneTowerSolar;
       	bool vortex = player.ZoneTowerVortex;
       	bool bloodMoon = Main.bloodMoon;
       	bool snowMoon = Main.snowMoon;
       	bool pumpkinMoon = Main.pumpkinMoon;
       	if (bloodMoon)
       	{
       		player.AddBuff(BuffID.Battle, 600);
       	}
       	if (snowMoon)
       	{
       		player.AddBuff(BuffID.RapidHealing, 600);
       	}
       	if (pumpkinMoon)
       	{
       		player.AddBuff(BuffID.WellFed, 600);
       	}
       	if (jungle)
       	{
       		player.AddBuff(BuffID.Thorns, 600);
       	}
       	else if (snow)
       	{
       		player.AddBuff(BuffID.Warmth, 600);
       	}
       	else if (beach)
       	{
       		player.AddBuff(BuffID.Wet, 600);
       	}
       	else if (corrupt)
       	{
       		player.AddBuff(BuffID.Wrath, 600);
       	}
       	else if (crimson)
       	{
       		player.AddBuff(BuffID.Rage, 600);
       	}
       	else if (dungeon)
       	{
       		player.AddBuff(BuffID.Dangersense, 600);
       	}
       	else if (desert)
       	{
       		player.AddBuff(BuffID.Endurance, 600);
       	}
       	else if (glow)
       	{
       		player.AddBuff(BuffID.Spelunker, 600);
       	}
       	else if (hell)
       	{
       		player.AddBuff(BuffID.Inferno, 600);
       	}
       	else if (holy)
       	{
       		player.AddBuff(BuffID.Heartreach, 600);
       	}
       	else if (nebula)
       	{
       		player.AddBuff(BuffID.MagicPower, 600);
       	}
       	else if (stardust)
       	{
       		player.AddBuff(BuffID.Summoning, 600);
       	}
       	else if (solar)
       	{
       		player.AddBuff(BuffID.Titan, 600);
       	}
       	else if (vortex)
       	{
       		player.AddBuff(BuffID.AmmoReservation, 600);
       	}
       	else
       	{
       		player.AddBuff(BuffID.DryadsWard, 600);
       	}
	}
}}
