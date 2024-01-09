using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class CosmicDischarge : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.autoReuse = true;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0.5f;
			Item.UseSound = SoundID.Item122;
			Item.value = 5000000;
			Item.shootSpeed = 24f;
			Item.shoot = Mod.Find<ModProjectile>("CosmicDischarge").Type;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(150, Main.DiscoG, 255);
	            }
	        }
	    }
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.2f : 0f) + //1.25
				(NPC.downedBoss1 ? 0.2f : 0f) + //1.5
				(NPC.downedBoss2 ? 0.3f : 0f) + //1.75
				(NPC.downedQueenBee ? 0.3f : 0f) + //2.25
				(NPC.downedBoss3 ? 0.3f : 0f) + //2.75
				(Main.hardMode ? 0.3f : 0f) + //4.25
				(NPC.downedMechBoss1 ? 0.3f : 0f) + //4.75
				(NPC.downedMechBoss2 ? 0.3f : 0f) + //5.25
				(NPC.downedMechBoss3 ? 0.3f : 0f) + //5.75
				(NPC.downedPlantBoss ? 0.3f : 0f) + //6.75
				(NPC.downedGolemBoss ? 0.3f : 0f) +
				(NPC.downedFishron ? 1f : 0f) +
				(NPC.downedAncientCultist ? 2f : 0f) + //12
				(NPC.downedMoonlord ? 10f : 0f) + //18
				(CalamityWorld.downedProvidence ? 20f : 0f) + //24
				(CalamityWorld.downedDoG ? 20f : 0f) + //31
				(CalamityWorld.downedYharon ? 40f : 0f); //55
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
			bool devourerOfGods = CalamityWorld.downedDoG;
	    	Item.useTime = devourerOfGods ? 15 : 20;
	    	Item.useAnimation = devourerOfGods ? 15 : 20;
	    	float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
	       	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("CosmicDischarge").Type, damage, knockback, player.whoAmI, 0.0f, ai3);
	    	return false;
		}
	}
}
