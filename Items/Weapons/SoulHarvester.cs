using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SoulHarvester : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Soul Harvester");
		//Tooltip.SetDefault("Enemies explode when on low health, spreading the plague");
	}

	public override void SetDefaults()
	{
		Item.width = 62;
		Item.damage = 101;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 20;
		Item.useTurn = true;
		Item.knockBack = 7.5f;
		Item.UseSound = SoundID.Item71;
		Item.autoReuse = true;
		Item.height = 64;
		Item.maxStack = 1;
		Item.value = 550000;
		Item.rare = ItemRarityID.Cyan;
		Item.shoot = Mod.Find<ModProjectile>("SoulScythe").Type;
		Item.shootSpeed = 18f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "PlagueCellCluster", 10);
		recipe.AddIngredient(ItemID.CursedFlame, 20);
		recipe.AddIngredient(ItemID.DeathSickle);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CursedTorch);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 200);
		target.AddBuff(BuffID.CursedInferno, 200);
		if (target.life <= (target.lifeMax * 0.15f))
		{
			SoundEngine.PlaySound(SoundID.Item14, player.position);
			Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HiveBombExplosion").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			player.position.X = player.position.X + (float)(player.width / 2);
			player.position.Y = player.position.Y + (float)(player.height / 2);
			player.position.X = player.position.X - (float)(player.width / 2);
			player.position.Y = player.position.Y - (float)(player.height / 2);
			for (int num621 = 0; num621 < 30; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 50; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
		}
	}
}}
