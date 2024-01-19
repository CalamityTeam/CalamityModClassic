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
	public class SHPC : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.damage = 11;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 96;
	        Item.height = 34;
	        Item.useTime = 50;
	        Item.useAnimation = 50;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
	        Item.value = 5000000;
	        Item.UseSound = SoundID.Item92;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("SHPB").Type;
	        Item.shootSpeed = 16f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, Main.DiscoG, 155);
	            }
	        }
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-25, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 7;
	    		Item.useAnimation = 7;
	    		Item.mana = 6;
	        	Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point2/Sounds/Item/LaserCannon");
			}
			else
			{
				Item.useTime = 50;
	        	Item.useAnimation = 50;
	        	Item.mana = 20;
	        	Item.UseSound = SoundID.Item92;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.1f : 0f) + //1.25
				(NPC.downedBoss1 ? 0.2f : 0f) + //2
				(NPC.downedBoss2 ? 0.2f : 0f) + //2.75
				(NPC.downedQueenBee ? 0.2f : 0f) + //3.25
				(NPC.downedBoss3 ? 0.2f : 0f) + //3.75
				(Main.hardMode ? 0.2f : 0f) + //4.75
				(NPC.downedPlantBoss ? 0.1f : 0f) + //4.85
				(NPC.downedGolemBoss ? 0.1f : 0f) + //5
				(NPC.downedAncientCultist ? 1f : 0f) + //8.5
				(NPC.downedMoonlord ? 2f : 0f) + //40
				(CalamityWorld.downedProvidence ? 3f : 0f) + //50
				(CalamityWorld.downedDoG ? 2f : 0f) + //60
				(CalamityWorld.downedYharon ? 15f : 0f); //75
			damage.Base = (int)((double)damage.Multiplicative * damageMult);
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
				(NPC.downedMoonlord ? 0.25f : 0f) +
				(CalamityWorld.downedProvidence ? 0.25f : 0f) +
				(CalamityWorld.downedDoG ? 0.5f : 0f) +
				(CalamityWorld.downedYharon ? 1f : 0f);
			knockback = knockback * kbMult;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			bool destroyer = NPC.downedMechBoss1;
	    	if (player.altFunctionUse == 2)
	    	{
	    		int fire = destroyer ? 3 : 1;
	        	for (int shootAmt = 0; shootAmt < fire; shootAmt++)
	        	{
	        		float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
		    		float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	        		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SHPL").Type, damage, (knockback * 0.5f), player.whoAmI, 0.0f, 0.0f);
	        	}
	    		return false;
	    	}
	    	else
	    	{
	    		int fire = destroyer ? 3 : 1;
	        	for (int shootAmt = 0; shootAmt < fire; shootAmt++)
	        	{
	        		float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
		    		float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	        		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SHPB").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        	}
	    		return false;
	    	}
		}
	}
}