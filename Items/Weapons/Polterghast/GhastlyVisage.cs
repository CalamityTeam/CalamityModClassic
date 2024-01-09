using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Polterghast
{
	public class GhastlyVisage : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ghastly Visage");
			//Tooltip.SetDefault("Fires homing ghast energy that explodes");
		}


	    public override void SetDefaults()
	    {
	        Item.damage = 150;
	        Item.DamageType = DamageClass.Magic;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.mana = 30;
	        Item.width = 78;
	        Item.height = 70;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
	        Item.value = 1000000;
	        Item.shootSpeed = 9f;
	        Item.shoot = Mod.Find<ModProjectile>("GhastlyVisage").Type;
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
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("GhastlyVisage").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}