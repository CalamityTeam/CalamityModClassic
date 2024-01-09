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
	public class DaemonsFlame : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Daemon's Flame");
			//Tooltip.SetDefault("Shoots daemon flame arrows as well as regular arrows");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 140;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.knockBack = 4f;
	        Item.value = 1000000;
	        Item.UseSound = SoundID.Item5;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Ranged;
			Item.channel = true;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("DaemonsFlame").Type;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 40;
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
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("DaemonsFlame").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
	    }
	}
}