using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Bumblebirb
{
	public class RougeSlash : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rouge Slash");
			//Tooltip.SetDefault("Fires a wave of 3 rouge air slashes");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 180;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 55;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item91;
			Item.value = 1000000;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("RougeSlashLarge").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 24f;
		}
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        int slash1 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("RougeSlashLarge").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        int slash2 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.8f, velocity.Y * 0.8f, Mod.Find<ModProjectile>("RougeSlashMedium").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        int slash3 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.6f, velocity.Y * 0.6f, Mod.Find<ModProjectile>("RougeSlashSmall").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}