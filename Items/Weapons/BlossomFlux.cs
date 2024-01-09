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
	public class BlossomFlux : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.damage = 2;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 40;
	        Item.height = 62;
	        Item.useTime = 4;
	        Item.useAnimation = 16;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 0.15f;
	        Item.value = 5000000;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("LeafArrow").Type;
	        Item.shootSpeed = 10f;
	        Item.useAmmo = 40;
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
	    	bool plantera = NPC.downedPlantBoss;
			if (player.altFunctionUse == 2)
			{
				Item.useTime = plantera ? 25 : 35;
	    		Item.useAnimation = plantera ? 25 : 35;
	        	Item.UseSound = SoundID.Item77;
			}
			else
			{
				Item.useTime = plantera ? 2 : 4;
	        	Item.useAnimation = 16;
	        	Item.UseSound = SoundID.Item5;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.5f : 0f) + //1.5
				(NPC.downedBoss1 ? 0.5f : 0f) + //2.25
				(NPC.downedBoss2 ? 0.5f : 0f) + //2.25
				(NPC.downedQueenBee ? 0.5f : 0f) + //3
				(NPC.downedBoss3 ? 0.5f : 0f) + //4
				(Main.hardMode ? 2f : 0f) + //8
				(NPC.downedMechBoss1 ? 0.5f : 0f) + //12
				(NPC.downedMechBoss2 ? 0.5f : 0f) +
				(NPC.downedMechBoss3 ? 0.5f : 0f) +
				(NPC.downedAncientCultist ? 5f : 0f) + //25
				(NPC.downedMoonlord ? 20f : 0f) + //40
				(CalamityWorld.downedProvidence ? 20f : 0f) + //50
				(CalamityWorld.downedDoG ? 20f : 0f) + //60
				(CalamityWorld.downedYharon ? 50f : 0f); //75
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
				(CalamityWorld.downedProvidence ? 0.25f : 0f) +
				(CalamityWorld.downedDoG ? 0.5f : 0f) +
				(CalamityWorld.downedYharon ? 0.5f : 0f);
			knockback = knockback * kbMult;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	        	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SporeBomb").Type, (int)((double)damage * 4f), (knockback * 3f), player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	        	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("LeafArrow").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	}
}