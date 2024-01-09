using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class BrinyBaron : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
			Item.value = 5000000;
			Item.shootSpeed = 4f;
			Item.shoot = Mod.Find<ModProjectile>("NobodyKnows").Type;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(53, Main.DiscoG, 255);
	            }
	        }
	    }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
	    	bool dukeFish = NPC.downedFishron;
			if (player.altFunctionUse == 2)
			{
				Item.noMelee = true;
	        	Item.noUseGraphic = true;
	    		Item.useTime = dukeFish ? 30 : 35;
	    		Item.useAnimation = dukeFish ? 30 : 35;
	        	Item.UseSound = SoundID.Item84;
			}
			else
			{
				Item.noMelee = false;
	        	Item.noUseGraphic = false;
	    		Item.useTime = dukeFish ? 25 : 30;
	    		Item.useAnimation = dukeFish ? 25 : 30;
	        	Item.UseSound = SoundID.Item1;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.1f : 0f) + //1.5
				(NPC.downedBoss1 ? 0.1f : 0f) + //1.75
				(NPC.downedBoss2 ? 0.1f : 0f) + //2
				(NPC.downedQueenBee ? 0.15f : 0f) + //2.5
				(NPC.downedBoss3 ? 0.15f : 0f) + //3
				(Main.hardMode ? 0.35f : 0f) + //4.5
				(NPC.downedMechBoss1 ? 0.2f : 0f) + //5
				(NPC.downedMechBoss2 ? 0.2f : 0f) + //5.5
				(NPC.downedMechBoss3 ? 0.2f : 0f) + //6
				(NPC.downedPlantBoss ? 0.3f : 0f) + //7
				(NPC.downedGolemBoss ? 0.3f : 0f) + //8
				(NPC.downedFishron ? 0.3f : 0f) + //9
				(NPC.downedAncientCultist ? 1f : 0f) + //11
				(NPC.downedMoonlord ? 1f : 0f) + //14
				(CalamityWorld.downedProvidence ? 2f : 0f) + //21
				(CalamityWorld.downedDoG ? 2f : 0f) + //28
				(CalamityWorld.downedYharon ? 10f : 0f); //48
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
	        	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Razorwind").Type, (int)((double)damage * 0.65f), knockback, player.whoAmI, 0.0f, 0.0f);
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
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Flare_Blue, 0f, 0f, 100, new Color(53, Main.DiscoG, 255));
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	bool dukeFish = NPC.downedFishron;
	    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrinyTyphoonBubble").Type, hit.Damage, hit.Knockback, player.whoAmI);
		}
	}
}
