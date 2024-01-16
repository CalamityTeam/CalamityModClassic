using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SoulHarvester : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/SoulHarvester");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Soul Harvester");
		Item.width = 62;  //The width of the .png file in pixels divided by 2.
		Item.damage = 73;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.useTime = 30;
		Item.useTurn = true;
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item71;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 64;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Enemies explode when on low health, spreading the plague");
		Item.value = 550000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("SoulScythe").Type;
		Item.shootSpeed = 16f;
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
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
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
				int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 89, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 50; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 89, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 89, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
		}
	}
}}
