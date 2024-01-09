using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class FourSeasonsGalaxia : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Galaxia");
			//Tooltip.SetDefault("Fires different homing projectiles based on what biome you're in\nUpon hitting an enemy you are granted a buff based on what biome you're in\nProjectiles also change based on moon events");
		}

		public override void SetDefaults()
		{
			Item.width = 64;
			Item.damage = 325;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 9;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 64;
			Item.maxStack = 1;
			Item.value = 10000000;
			Item.shoot = Mod.Find<ModProjectile>("Galaxia").Type;
			Item.shootSpeed = 24f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "OmegaBiomeBlade");
			recipe.AddIngredient(null, "CosmiliteBar", 10);
			recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Dirt);
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
	}
}
