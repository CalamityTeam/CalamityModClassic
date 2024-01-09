using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class MirrorBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mirror Blade");
			//Tooltip.SetDefault("The amount of contact damage an enemy does is added to this weapons' damage\nYou must hit an enemy with the blade to trigger this effect\nConsumes mana to fire mirror blasts");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.damage = 250;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.mana = 5;
			Item.useAnimation = 14;
			Item.useTime = 14;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
			Item.value = 2000000;
			Item.shootSpeed = 20f;
	        Item.shoot = Mod.Find<ModProjectile>("MirrorBlast").Type;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BoneTorch);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	int conDamage = target.damage + 250;
	    	if (conDamage <= 250)
	    	{
	    		conDamage = 250;
	    	}
	    	Item.damage = conDamage;
		}
	}
}
