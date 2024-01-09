using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class AegisBlade : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.value = 5000000;
			Item.shootSpeed = 9f;
			Item.shoot = Mod.Find<ModProjectile>("NobodyKnows").Type;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, Main.DiscoG, 53);
	            }
	        }
	    }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			bool golem = NPC.downedGolemBoss;
			if (player.altFunctionUse == 2)
			{
	        	Item.noMelee = true;
	    		Item.mana = 4;
	    		Item.useTime = golem ? 20 : 25;
	    		Item.useAnimation = golem ? 20 : 25;
	        	Item.UseSound = SoundID.Item73;
			}
			else
			{
	        	Item.noMelee = false;
	    		Item.mana = 0;
	    		Item.useTime = golem ? 15 : 20;
	    		Item.useAnimation = golem ? 15 : 20;
	        	Item.UseSound = SoundID.Item1;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.1f : 0f) + //1.25
				(NPC.downedBoss1 ? 0.1f : 0f) + //1.5
				(NPC.downedBoss2 ? 0.1f : 0f) + //1.75
				(NPC.downedQueenBee ? 0.2f : 0f) + //2.25
				(NPC.downedBoss3 ? 0.2f : 0f) + //2.75
				(Main.hardMode ? 0.35f : 0f) + //4.25
				(NPC.downedMechBoss1 ? 0.2f : 0f) + //4.75
				(NPC.downedMechBoss2 ? 0.2f : 0f) + //5.25
				(NPC.downedMechBoss3 ? 0.2f : 0f) + //5.75
				(NPC.downedPlantBoss ? 0.35f : 0f) + //6.75
				(NPC.downedAncientCultist ? 2f : 0f) + //12
				(NPC.downedMoonlord ? 2f : 0f) + //18
				(CalamityWorld.downedProvidence ? 2f : 0f) + //24
				(CalamityWorld.downedDoG ? 2f : 0f) + //31
				(CalamityWorld.downedYharon ? 10f : 0f); //60
			damage.Base = (int)((double)damage.Base * damageMult);
	    }
		
		public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
		{
			float kbMult = 1f +
				(NPC.downedSlimeKing ? 0.1f : 0f) +
				(NPC.downedBoss1 ? 0.1f : 0f) + 
				(NPC.downedBoss2 ? 0.1f : 0f) + 
				(NPC.downedQueenBee ? 0.15f : 0f) +
				(NPC.downedBoss3 ? 0.15f : 0f) +
				(Main.hardMode ? 0.15f : 0f) +
				(NPC.downedMechBossAny ? 0.1f : 0f) +
				(NPC.downedPlantBoss ? 0.15f : 0f) +
				(NPC.downedGolemBoss ? 0.1f : 0f) +
				(NPC.downedFishron ? 0.15f : 0f) +
				(NPC.downedAncientCultist ? 0.15f : 0f) +
				(NPC.downedMoonlord ? 0.35f : 0f) +
				(CalamityWorld.downedProvidence ? 0.15f : 0f) +
				(CalamityWorld.downedDoG ? 0.15f : 0f) +
				(CalamityWorld.downedYharon ? 0.2f : 0f);
			knockback = knockback * kbMult;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("AegisBeam").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	        	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("NobodyKnows").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GoldCoin, 0f, 0f, 0, new Color(255, Main.DiscoG, 53));
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("AegisBlast").Type, hit.Damage, hit.Knockback, Main.myPlayer);
	    	int num251 = Main.rand.Next(1, 3);
			for (int num252 = 0; num252 < num251; num252++)
			{
				Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				while (value15.X == 0f && value15.Y == 0f)
				{
					value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				}
				value15.Normalize();
				value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
				Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X + (float)(target.width / 2), target.Center.Y + (float)(target.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("AegisFlame").Type, hit.Damage, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
