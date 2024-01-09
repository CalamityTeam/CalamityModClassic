using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class DraedonsExoblade : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 80;
		Item.damage = 3280;
		Item.useAnimation = 16;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 16;
		Item.useTurn = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.knockBack = 6f;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 100;
		Item.value = 100000000;
		Item.shoot = Mod.Find<ModProjectile>("Exobeam").Type;
		Item.shootSpeed = 19f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
	{
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
    }
	
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		int lifeAmount = player.statLifeMax2 - player.statLife;
    	float damageAdd = (((float)lifeAmount * 5f) + 3280f);
    	damage.Base = (int)(damageAdd * player.GetDamage(DamageClass.Melee).Base);
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(4))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255));
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (target.life <= (target.lifeMax * 0.05f))
		{
			Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Exoboom").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			player.position.X = player.position.X + (float)(player.width / 2);
			player.position.Y = player.position.Y + (float)(player.height / 2);
			player.position.X = player.position.X - (float)(player.width / 2);
			player.position.Y = player.position.Y - (float)(player.height / 2);
			for (int num621 = 0; num621 < 5; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 10; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255), 2f);
				Main.dust[num624].velocity *= 2f;
			}
		}
    	if (Main.rand.NextBool(5))
    	{
    		target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 500);
    	}
    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 200);
        target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 200);
       	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 200);
       	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 200);
       	target.AddBuff(BuffID.CursedInferno, 200);
		target.AddBuff(BuffID.Frostburn, 200);
		target.AddBuff(BuffID.OnFire, 200);
		target.AddBuff(BuffID.Ichor, 200);
    	SoundEngine.PlaySound(SoundID.Item88, player.position);
    	float xPos = (Main.rand.NextBool(2)) ? player.position.X + 800 : player.position.X - 800;
    	Vector2 vector2 = new Vector2(xPos, player.position.Y + Main.rand.Next(-800, 801));
    	float num80 = xPos;
    	float velocityX = (float)target.position.X - vector2.X;
    	float velocityY = (float)target.position.Y - vector2.Y;
    	float dir= (float)Math.Sqrt((double)(velocityX * velocityX + velocityY * velocityY));
    	dir = 10 / num80;
    	velocityX *= dir * 150;
    	velocityY *= dir * 150;
    	if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Exocomet").Type] < 8)
    	{
	    	for (int comet = 0; comet < 2; comet++)
	    	{
	    		float ai1 = (Main.rand.NextFloat() + 0.5f);
	    		Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, velocityX, velocityY, Mod.Find<ModProjectile>("Exocomet").Type, hit.Damage, hit.Knockback, player.whoAmI, 0.0f, ai1);
	    	}
    	}
    	if (target.type == NPCID.TargetDummy)
		{
			return;
		}
    	int healAmount = (Main.rand.Next(10) + 5);
    	player.statLife += healAmount;
    	player.HealEffect(healAmount);
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "Terratomere");
		recipe.AddIngredient(null, "AnarchyBlade");
		recipe.AddIngredient(null, "BalefulHarvester");
		recipe.AddIngredient(null, "CelestialClaymore");
		recipe.AddIngredient(null, "FlarefrostBlade");
		recipe.AddIngredient(null, "PhoenixBlade");
		recipe.AddIngredient(null, "StellarStriker");
		recipe.AddIngredient(null, "Terracotta");
		recipe.AddIngredient(null, "NightmareFuel", 10);
        recipe.AddIngredient(null, "EndothermicEnergy", 10);
		recipe.AddIngredient(null, "CosmiliteBar", 10);
		recipe.AddIngredient(null, "Phantoplasm", 50);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}
