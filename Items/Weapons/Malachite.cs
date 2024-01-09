using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Malachite : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.damage = 9;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 58;
			Item.value = 5000000;
			Item.shoot = Mod.Find<ModProjectile>("Malachite").Type;
			Item.shootSpeed = 10f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, 203, 103);
	            }
	        }
	    }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
	    	bool plague = CalamityWorld.downedPlaguebringer;
			if (player.altFunctionUse == 2)
			{
				Item.useTime = plague ? 10 : 20;
	    		Item.useAnimation = plague ? 10 : 20;
	        	Item.UseSound = SoundID.Item109;
			}
			else
			{
				Item.useTime = plague ? 5 : 10;
	    		Item.useAnimation = plague ? 5 : 10;
	        	Item.UseSound = SoundID.Item1;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.2f : 0f) + //1.5
				(NPC.downedBoss1 ? 0.2f : 0f) + //2
				(NPC.downedBoss2 ? 0.2f : 0f) + //2.5
				(NPC.downedQueenBee ? 0.2f : 0f) + //3.5
				(NPC.downedBoss3 ? 0.2f : 0f) + //4.5
				(Main.hardMode ? 0.35f : 0f) + //4.25
				(NPC.downedMechBoss1 ? 0.1f : 0f) + //4.75
				(NPC.downedMechBoss2 ? 0.1f : 0f) + //5.25
				(NPC.downedMechBoss3 ? 0.1f : 0f) + //5.75
				(NPC.downedPlantBoss ? 0.3f : 0f) + //6.75
				(NPC.downedGolemBoss ? 0.3f : 0f) +
				(NPC.downedAncientCultist ? 1.5f : 0f) + //12
				(NPC.downedMoonlord ? 4f : 0f) + //18
				(CalamityWorld.downedProvidence ? 8f : 0f) + //24
				(CalamityWorld.downedDoG ? 8f : 0f) + //31
				(CalamityWorld.downedYharon ? 20f : 0f); //55
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
	    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("MalachiteBolt").Type, (int)((double)damage * 2.5f), knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	        	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Malachite").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	}
}
